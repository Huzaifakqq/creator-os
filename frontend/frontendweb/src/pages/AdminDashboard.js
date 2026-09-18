import { useState, useEffect } from "react";
import { useAuth } from "../context/AuthContext";
import "../styles/dashboard.css";

const adminSidebar = [
  { icon: "dashboard", label: "Dashboard", active: true },
  { icon: "people", label: "Users" },
  { icon: "business", label: "Workspaces" },
  { icon: "shopping_cart", label: "Orders" },
  { icon: "storefront", label: "Marketplace" },
  { icon: "analytics", label: "Analytics" },
  { icon: "security", label: "Security" },
  { icon: "settings", label: "Settings" },
];

const recentActivity = [
  { icon: "person_add", text: "New user registered — test@creatoros.com", sub: "Joined as Member", time: "2m ago" },
  { icon: "business", text: "New workspace created — John's Academy", sub: "Creator plan • Free tier", time: "15m ago" },
  { icon: "shopping_cart", text: "New order #1084 — $149 revenue", sub: "Stripe payment processed", time: "1h ago" },
  { icon: "flag", text: "Dispute opened — Order #1072", sub: "Customer requesting refund", time: "2h ago" },
  { icon: "person_add", text: "New user registered — demo@creatoros.com", sub: "Joined as Creator", time: "3h ago" },
];

export default function AdminDashboard() {
  const { user, logout } = useAuth();
  const [stats, setStats] = useState(null);

  useEffect(() => {
    fetch("http://localhost:5083/api/admin/dashboard", {
      headers: { Authorization: `Bearer ${localStorage.getItem("token")}` },
    })
      .then((res) => res.json())
      .then((data) => setStats(data))
      .catch(() => {});
  }, []);

  const hour = new Date().getHours();
  const greeting = hour < 12 ? "Good morning" : hour < 18 ? "Good afternoon" : "Good evening";
  const firstName = user?.DisplayName?.split(" ")[0] || user?.email?.split("@")[0] || "Admin";

  return (
    <div className="dashboard">
      {/* Sidebar */}
      <aside className="sidebar">
        <div className="sidebar-top">
          <div className="sidebar-brand">
            <div className="brand-icon" style={{ background: "linear-gradient(135deg, #ff7849, #ff4d4d)" }}>A</div>
            <div>
              <div className="brand-name">Creator OS</div>
              <div className="brand-sub" style={{ color: "#ff7849" }}>Platform Admin</div>
            </div>
          </div>

          <nav className="sidebar-nav">
            {adminSidebar.map((item) => (
              <a
                key={item.label}
                className={`sidebar-link ${item.active ? "active" : ""}`}
                href="#"
                onClick={(e) => e.preventDefault()}
              >
                <span className="material-symbols-outlined">{item.icon}</span>
                {item.label}
              </a>
            ))}
          </nav>
        </div>

        <div className="sidebar-bottom">
          <a className="sidebar-link" href="#" onClick={(e) => e.preventDefault()}>
            <span className="material-symbols-outlined">description</span>
            Documentation
          </a>
          <div className="sidebar-tenant" onClick={logout} style={{ cursor: "pointer" }}>
            <div className="tenant-avatar" style={{ background: "linear-gradient(135deg, #ff7849, #ff4d4d)" }}>
              {user?.DisplayName?.[0] || "A"}
            </div>
            <div>
              <div className="tenant-name">{user?.DisplayName || "Admin"}</div>
              <div className="tenant-plan" style={{ color: "#ff7849" }}>Super Admin</div>
            </div>
          </div>
        </div>
      </aside>

      {/* Main Content */}
      <div className="main-area">
        <header className="topbar">
          <div className="search-bar">
            <span className="material-symbols-outlined">search</span>
            <input type="text" placeholder="Search users, workspaces, orders..." />
            <kbd>⌘K</kbd>
          </div>
          <div className="topbar-right">
            <button className="topbar-btn">
              <span className="material-symbols-outlined">help</span>
              Quick Help
            </button>
            <button className="topbar-icon">
              <span className="material-symbols-outlined">notifications</span>
            </button>
            <div className="user-menu" onClick={logout} title="Click to logout">
              <div className="user-avatar" style={{ background: "linear-gradient(135deg, #ff7849, #ff4d4d)" }}>
                {user?.DisplayName?.[0] || "A"}
              </div>
              <div>
                <div className="user-name">{user?.DisplayName || "Admin"}</div>
                <div className="user-role">Super Admin</div>
              </div>
            </div>
          </div>
        </header>

        <main className="content">
          <div className="greeting-section">
            <div>
              <h1 className="greeting-title">{greeting}, {firstName}! 🛡️</h1>
              <p className="greeting-date">
                {new Date().toLocaleDateString("en-US", { weekday: "long", month: "long", day: "numeric", year: "numeric" })}
              </p>
              <p className="greeting-growth">Platform status: All systems operational</p>
            </div>
            <div className="greeting-actions">
              <button className="btn-outline">
                <span className="material-symbols-outlined">download</span>
                Export Data
              </button>
              <button className="btn-primary-dash">
                <span className="material-symbols-outlined">settings</span>
                Platform Settings
              </button>
            </div>
          </div>

          {/* Stats */}
          <div className="stats-grid">
            <div className="stat-card">
              <div className="stat-header">
                <span className="stat-label">Total Users</span>
                <span className="stat-icon blue">
                  <span className="material-symbols-outlined">people</span>
                </span>
              </div>
              <div className="stat-value">{stats?.totalUsers ?? "—"}</div>
              <div className="stat-change positive">Registered accounts</div>
            </div>
            <div className="stat-card">
              <div className="stat-header">
                <span className="stat-label">Active Workspaces</span>
                <span className="stat-icon purple">
                  <span className="material-symbols-outlined">business</span>
                </span>
              </div>
              <div className="stat-value">{stats?.totalTenants ?? "—"}</div>
              <div className="stat-change positive">Creator workspaces</div>
            </div>
            <div className="stat-card">
              <div className="stat-header">
                <span className="stat-label">Total Orders</span>
                <span className="stat-icon green">
                  <span className="material-symbols-outlined">shopping_cart</span>
                </span>
              </div>
              <div className="stat-value">{stats?.totalOrders ?? "—"}</div>
              <div className="stat-change positive">Platform-wide orders</div>
            </div>
            <div className="stat-card">
              <div className="stat-header">
                <span className="stat-label">Total Revenue</span>
                <span className="stat-icon orange">
                  <span className="material-symbols-outlined">account_balance_wallet</span>
                </span>
              </div>
              <div className="stat-value">{stats?.totalRevenueFormatted ?? "$0.00"}</div>
              <div className="stat-change positive">Platform GMV</div>
            </div>
          </div>

          {/* Quick Admin Tools */}
          <div className="section">
            <h3 className="section-title">Quick Actions</h3>
            <div className="tools-grid">
              <a className="tool-card" href="#" onClick={(e) => e.preventDefault()}>
                <span className="material-symbols-outlined tool-icon">people</span>
                <div>
                  <div className="tool-label">Manage Users</div>
                  <div className="tool-desc">View, edit, ban users</div>
                </div>
              </a>
              <a className="tool-card" href="#" onClick={(e) => e.preventDefault()}>
                <span className="material-symbols-outlined tool-icon">business</span>
                <div>
                  <div className="tool-label">Manage Workspaces</div>
                  <div className="tool-desc">View all tenant workspaces</div>
                </div>
              </a>
              <a className="tool-card" href="#" onClick={(e) => e.preventDefault()}>
                <span className="material-symbols-outlined tool-icon">storefront</span>
                <div>
                  <div className="tool-label">Marketplace</div>
                  <div className="tool-desc">Approve or reject listings</div>
                </div>
              </a>
              <a className="tool-card" href="#" onClick={(e) => e.preventDefault()}>
                <span className="material-symbols-outlined tool-icon">flag</span>
                <div>
                  <div className="tool-label">Disputes</div>
                  <div className="tool-desc">Handle refund disputes</div>
                </div>
              </a>
              <a className="tool-card" href="#" onClick={(e) => e.preventDefault()}>
                <span className="material-symbols-outlined tool-icon">security</span>
                <div>
                  <div className="tool-label">Audit Logs</div>
                  <div className="tool-desc">Platform activity logs</div>
                </div>
              </a>
              <a className="tool-card" href="#" onClick={(e) => e.preventDefault()}>
                <span className="material-symbols-outlined tool-icon">analytics</span>
                <div>
                  <div className="tool-label">Analytics</div>
                  <div className="tool-desc">Platform-wide metrics</div>
                </div>
              </a>
            </div>
          </div>

          {/* Activity + Side panels */}
          <div className="three-col">
            <div className="activity-card">
              <div className="card-header">
                <h3>Recent Platform Activity</h3>
                <a href="#" onClick={(e) => e.preventDefault()}>View All →</a>
              </div>
              <div className="activity-list">
                {recentActivity.map((item, i) => (
                  <div key={i} className="activity-item">
                    <span className="material-symbols-outlined activity-icon">{item.icon}</span>
                    <div className="activity-content">
                      <div className="activity-text">{item.text}</div>
                      <div className="activity-sub">{item.sub}</div>
                    </div>
                    <span className="activity-time">{item.time}</span>
                  </div>
                ))}
              </div>
            </div>

            <div className="right-col">
              <div className="products-card">
                <div className="card-header">
                  <h3>Platform Health</h3>
                  <span className="status-dot"></span>
                </div>
                <div className="limits-list">
                  <div className="limit-item">
                    <span className="material-symbols-outlined">dns</span>
                    <span className="limit-label">API Server</span>
                    <span className="limit-value" style={{ color: "var(--tertiary)" }}>Online</span>
                  </div>
                  <div className="limit-item">
                    <span className="material-symbols-outlined">storage</span>
                    <span className="limit-label">Database</span>
                    <span className="limit-value" style={{ color: "var(--tertiary)" }}>Healthy</span>
                  </div>
                  <div className="limit-item">
                    <span className="material-symbols-outlined">speed</span>
                    <span className="limit-label">Redis Cache</span>
                    <span className="limit-value" style={{ color: "var(--tertiary)" }}>Connected</span>
                  </div>
                  <div className="limit-item">
                    <span className="material-symbols-outlined">cloud</span>
                    <span className="limit-label">CDN</span>
                    <span className="limit-value" style={{ color: "var(--tertiary)" }}>Active</span>
                  </div>
                </div>
              </div>

              <div className="limits-card">
                <div className="card-header">
                  <h3>Recent Signups</h3>
                </div>
                <div className="products-list">
                  <div className="product-item">
                    <div className="product-info">
                      <div className="product-name">Hafsa Ahmed</div>
                      <span className="product-tag" style={{ background: "#7c5cfc22", color: "#7c5cfc" }}>Member</span>
                    </div>
                    <div className="product-right">
                      <span className="product-extra">2h ago</span>
                    </div>
                  </div>
                  <div className="product-item">
                    <div className="product-info">
                      <div className="product-name">Huzaifa Kashif</div>
                      <span className="product-tag" style={{ background: "#4d8eff22", color: "#4d8eff" }}>Creator</span>
                    </div>
                    <div className="product-right">
                      <span className="product-extra">5h ago</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </main>
      </div>
    </div>
  );
}
