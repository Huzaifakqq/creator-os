import { createContext, useContext, useState, useEffect } from "react";
import { auth } from "../services/api";

const AuthContext = createContext(null);

export function AuthProvider({ children }) {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(localStorage.getItem("token"));
  const [tenants, setTenants] = useState([]);
  const [currentTenant, setCurrentTenant] = useState(null);
  const [isAdmin, setIsAdmin] = useState(localStorage.getItem("isAdmin") === "true");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    if (token) {
      auth
        .me(token)
        .then((data) => {
          setUser(data);
          const savedTenant = localStorage.getItem("currentTenant");
          if (savedTenant) {
            setCurrentTenant(JSON.parse(savedTenant));
          }
        })
        .catch(() => {
          localStorage.removeItem("token");
          localStorage.removeItem("currentTenant");
          setToken(null);
          setUser(null);
        })
        .finally(() => setLoading(false));
    } else {
      setLoading(false);
    }
  }, [token]);

  const login = async (email, password) => {
    console.log("[Auth] login called");
    const data = await auth.login(email, password);

    if (data.isAdmin) {
      localStorage.setItem("token", data.token);
      localStorage.setItem("isAdmin", "true");
      setToken(data.token);
      setIsAdmin(true);
      setUser({ id: data.userId, email, isAdmin: true });
      console.log("[Auth] admin login done");
      return data;
    }

    localStorage.removeItem("isAdmin");
    setIsAdmin(false);

    // Always set user regardless of token (multi-tenant users get token=null initially)
    setUser({ id: data.userId, email });

    if (data.token) {
      localStorage.setItem("token", data.token);
      setToken(data.token);

      if (data.tenants && data.tenants.length === 1) {
        setCurrentTenant(data.tenants[0]);
        localStorage.setItem("currentTenant", JSON.stringify(data.tenants[0]));
      } else if (data.tenants && data.tenants.length > 1) {
        setTenants(data.tenants);
      } else if (!data.tenants || data.tenants.length === 0) {
        setTenants([]);
      }
    } else if (data.tenants && data.tenants.length > 0) {
      // Multi-tenant: no token yet, store tenants for SelectTenant screen
      setTenants(data.tenants);
    }

    console.log("[Auth] login done", { hasToken: !!data.token, tenantCount: data.tenants?.length || 0 });
    return data;
  };

  const register = async (email, password, displayName, role, tenantName) => {
    console.log("[Auth] register called", { role });
    const data = await auth.register(email, password, displayName, role, tenantName);
    console.log("[Auth] register done");
    return data;
  };

  const selectTenant = async (tenantId) => {
    console.log("[Auth] selectTenant called", tenantId);
    const data = await auth.selectTenant(tenantId);
    localStorage.setItem("token", data.token);
    setToken(data.token);

    const tenant = tenants.find((t) => t.tenantId === tenantId);
    if (tenant) {
      setCurrentTenant(tenant);
      localStorage.setItem("currentTenant", JSON.stringify(tenant));
    }

    setUser((prev) => ({ ...prev }));
    console.log("[Auth] selectTenant done");
    return data;
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("currentTenant");
    localStorage.removeItem("isAdmin");
    setToken(null);
    setUser(null);
    setCurrentTenant(null);
    setTenants([]);
    setIsAdmin(false);
  };

  const joinWorkspace = async (workspaceSlug) => {
    console.log("[Auth] joinWorkspace called", workspaceSlug);
    const data = await auth.joinWorkspace(workspaceSlug);
    if (data.success || data.Success) {
      localStorage.setItem("token", data.token);
      setToken(data.token);
      const tenant = { tenantId: data.tenantId, name: data.tenantName, role: data.role };
      setCurrentTenant(tenant);
      localStorage.setItem("currentTenant", JSON.stringify(tenant));
    }
    console.log("[Auth] joinWorkspace done");
    return data;
  };

  return (
    <AuthContext.Provider value={{ user, token, tenants, currentTenant, isAdmin, loading, login, register, selectTenant, joinWorkspace, logout }}>
      {children}
    </AuthContext.Provider>
  );
}

export function useAuth() {
  return useContext(AuthContext);
}
