import { useState, useEffect } from "react";
import { AuthProvider, useAuth } from "./context/AuthContext";
import Login from "./pages/Login";
import Register from "./pages/Register";
import ForgotPassword from "./pages/ForgotPassword";
import Dashboard from "./pages/Dashboard";
import AdminDashboard from "./pages/AdminDashboard";
import "./styles/global.css";
import "./styles/login.css";

function SelectTenant() {
  const { tenants, selectTenant } = useAuth();
  const [loading, setLoading] = useState(false);

  const handleSelect = async (tenantId) => {
    setLoading(true);
    try {
      await selectTenant(tenantId);
    } catch (err) {
      console.error("[SelectTenant] Error:", err.message);
      setLoading(false);
    }
  };

  return (
    <div className="login-page">
      <div className="login-bg">
        <div className="orb orb-1"></div>
        <div className="orb orb-2"></div>
        <div className="orb orb-3"></div>
        <div className="grid-overlay"></div>
      </div>
      <main className="login-main">
        <div className="login-header">
          <div className="login-badge"><span className="badge-dot"></span>Enterprise Node — v2.4 Active</div>
          <h1 className="login-title">Creator OS</h1>
          <p className="login-subtitle">Select a workspace to continue</p>
        </div>
        <section className="auth-card">
          <div className="auth-card-header">
            <h2 className="auth-card-title">Your Workspaces</h2>
            <p className="auth-card-subtitle">You have access to multiple workspaces</p>
          </div>
          <div className="tenant-list">
            {tenants.map((t) => (
              <button key={t.tenantId} className="tenant-select-btn" onClick={() => handleSelect(t.tenantId)} disabled={loading}>
                <div className="tenant-select-avatar">{t.name[0].toUpperCase()}</div>
                <div className="tenant-select-info">
                  <div className="tenant-select-name">{t.name}</div>
                  <div className="tenant-select-role">{t.role}</div>
                </div>
                <span className="material-symbols-outlined">arrow_forward</span>
              </button>
            ))}
          </div>
        </section>
      </main>
    </div>
  );
}

function getPageFromHash() {
  const hash = window.location.hash.replace("#", "");
  if (["login", "register", "forgot"].includes(hash)) return hash;
  return null;
}

function AppRoutes() {
  const { user, loading, currentTenant, tenants, isAdmin } = useAuth();
  const [page, setPage] = useState(getPageFromHash() || "login");

  useEffect(() => {
    const handler = () => {
      const newPage = getPageFromHash();
      if (newPage) setPage(newPage);
    };
    window.addEventListener("popstate", handler);
    return () => window.removeEventListener("popstate", handler);
  }, []);

  const navigate = (newPage) => {
    window.location.hash = newPage;
    setPage(newPage);
  };

  if (loading) {
    return (
      <div className="login-page">
        <div className="login-bg">
          <div className="orb orb-1"></div>
          <div className="orb orb-2"></div>
          <div className="orb orb-3"></div>
          <div className="grid-overlay"></div>
        </div>
        <main className="login-main">
          <p style={{ color: "#fff", textAlign: "center" }}>Loading...</p>
        </main>
      </div>
    );
  }

  if (user && isAdmin) {
    return <AdminDashboard />;
  }

  if (user && !currentTenant && tenants.length > 1) {
    return <SelectTenant />;
  }

  if (user) {
    return <Dashboard />;
  }

  return (
    <>
      {page === "login" && <Login onSwitch={navigate} />}
      {page === "register" && <Register onSwitch={navigate} />}
      {page === "forgot" && <ForgotPassword onSwitch={navigate} />}
    </>
  );
}

export default function App() {
  return (
    <AuthProvider>
      <AppRoutes />
    </AuthProvider>
  );
}
