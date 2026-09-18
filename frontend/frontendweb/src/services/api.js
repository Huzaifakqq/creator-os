const API_BASE = "http://localhost:5083/api";

async function request(endpoint, options = {}) {
  const token = localStorage.getItem("token");

  const config = {
    headers: {
      "Content-Type": "application/json",
      ...options.headers,
    },
    ...options,
  };

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  const url = `${API_BASE}${endpoint}`;
  console.log(`[API] ${options.method || "GET"} ${url}`);

  try {
    const response = await fetch(url, config);
    console.log(`[API] Response: ${response.status}`);

    const body = await response.json().catch(() => null);

    if (!response.ok) {
      console.error(`[API] Error ${response.status}:`, body);
      throw new Error(body?.error || body?.Error || `Request failed (HTTP ${response.status})`);
    }

    console.log(`[API] Success`);
    return body;
  } catch (err) {
    console.error(`[API] Fetch failed:`, err.message);
    throw err;
  }
}

export const auth = {
  login: (email, password) =>
    request("/auth/login", {
      method: "POST",
      body: JSON.stringify({ email, password }),
    }),
  register: (email, password, displayName, role, tenantName) =>
    request("/auth/register", {
      method: "POST",
      body: JSON.stringify({ email, password, displayName, role, tenantName }),
    }),
  me: (token) =>
    request("/auth/me", {
      headers: { Authorization: `Bearer ${token}` },
    }),
  selectTenant: (tenantId) =>
    request("/auth/select-tenant", {
      method: "POST",
      body: JSON.stringify({ tenantId }),
    }),
  joinWorkspace: (workspaceSlug) =>
    request("/auth/join-workspace", {
      method: "POST",
      body: JSON.stringify({ workspaceSlug }),
    }),
};

export const products = {
  getAll: () => request("/products"),
  getById: (id) => request(`/products/${id}`),
  create: (data) =>
    request("/products", {
      method: "POST",
      body: JSON.stringify(data),
    }),
  update: (id, data) =>
    request(`/products/${id}`, {
      method: "PUT",
      body: JSON.stringify(data),
    }),
  delete: (id) =>
    request(`/products/${id}`, { method: "DELETE" }),
};

export const orders = {
  getAll: () => request("/orders"),
  getById: (id) => request(`/orders/${id}`),
};

export const analytics = {
  overview: () => request("/analytics/overview"),
  revenue: (days) => request(`/analytics/revenue?days=${days || 30}`),
  products: () => request("/analytics/products"),
};

export const marketplace = {
  workspaces: () => request("/marketplace/workspaces"),
};

export default { auth, products, orders, analytics, marketplace };
