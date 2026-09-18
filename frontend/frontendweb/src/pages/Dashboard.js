import { useState, useEffect } from "react";
import { useAuth } from "../context/AuthContext";
import { analytics as analyticsApi } from "../services/api";
import Products from "./Products";
import "../styles/dashboard.css";

const creatorSidebar = [
  { icon: "dashboard", label: "Dashboard", active: true },
  { icon: "workspaces", label: "Workspaces" },
  { icon: "web", label: "Website Builder" },
  { icon: "widgets", label: "App Builder" },
  { icon: "shopping_cart", label: "Commerce" },
  { icon: "storefront", label: "Marketplace" },
  { icon: "school", label: "Courses" },
  { icon: "card_membership", label: "Memberships" },
  { icon: "fitness_center", label: "Coaching" },
  { icon: "contacts", label: "CRM" },
  { icon: "mail", label: "Email" },
  { icon: "smart_toy", label: "AI Agents" },
  { icon: "analytics", label: "Analytics" },
  { icon: "campaign", label: "Affiliates" },
  { icon: "settings", label: "Settings" },
];

const memberSidebar = [
  { icon: "dashboard", label: "Dashboard", active: true },
  { icon: "school", label: "My Courses" },
  { icon: "card_membership", label: "My Memberships" },
  { icon: "fitness_center", label: "My Coaching" },
  { icon: "shopping_cart", label: "My Orders" },
  { icon: "settings", label: "Settings" },
];

const browseSidebar = [
  { icon: "dashboard", label: "Dashboard", active: true },
  { icon: "storefront", label: "Browse Workspaces" },
  { icon: "school", label: "Courses" },
  { icon: "card_membership", label: "Memberships" },
  { icon: "settings", label: "Settings" },
];

const creatorQuickTools = [
  { icon: "web", label: "Create Website", desc: "Launch modern pages" },
  { icon: "add_shopping_cart", label: "Add Product", desc: "Digital kits & services" },
  { icon: "school", label: "Launch Course", desc: "Modules & cohorts" },
  { icon: "mail", label: "Send Campaign", desc: "Broadcast to audience" },
  { icon: "smart_toy", label: "Deploy Agent", desc: "Autonomous workflow" },
  { icon: "storefront", label: "View Marketplace", desc: "Browse plugins" },
];

const memberQuickTools = [
  { icon: "school", label: "Browse Courses", desc: "Discover new skills" },
  { icon: "card_membership", label: "Memberships", desc: "Manage subscriptions" },
  { icon: "fitness_center", label: "Coaching", desc: "Book sessions" },
  { icon: "shopping_cart", label: "Marketplace", desc: "Browse products" },
  { icon: "help", label: "Help Center", desc: "Get support" },
  { icon: "group", label: "Community", desc: "Connect with others" },
];

const recentActivityCreator = [
  { icon: "shopping_cart", text: "New order #1084 — Sarah Miller bought SaaS Starter Kit ($149)", sub: "Processed via Stripe • Instant Digital Delivery", time: "4m ago" },
  { icon: "card_membership", text: "New Subscriber — David Chen subscribed to Pro Membership ($29/mo)", sub: "Community access granted • Recurring billing", time: "22m ago" },
  { icon: "school", text: "Course Completed — Elena Rostova completed AI Automation Mastery", sub: "Certificate #8841 generated automatically", time: "1h ago" },
  { icon: "smart_toy", text: "AI Agent Triggered — Growth Agent published 3 scheduled LinkedIn posts", sub: "Engagement tracking activated across threads", time: "2h ago" },
  { icon: "campaign", text: "Affiliate Payout — $320 paid to Marcus Vance", sub: "8 conversions attributed • Transfer successful", time: "4h ago" },
];

const recentActivityMember = [
  { icon: "school", text: "You completed Lesson 8 in Advanced Marketing", sub: "Progress: 80% complete", time: "1h ago" },
  { icon: "card_membership", text: "Pro Membership renewed", sub: "Next billing: October 24, 2025 • $29/mo", time: "1d ago" },
  { icon: "shopping_cart", text: "You purchased Design System Pack ($49)", sub: "Download link sent to your email", time: "3d ago" },
];

const topProducts = [
  { name: "Mastering Next.js & AI", tag: "Course", rev: "$4,890", extra: "128 sales", tagColor: "#4d8eff" },
  { name: "Creator OS UI Kit", tag: "Digital Asset", rev: "$3,420", extra: "245 sales", tagColor: "#7c5cfc" },
  { name: "Inner Circle VIP", tag: "Membership", rev: "$2,900", extra: "58 members", tagColor: "#4edea3" },
  { name: "1-on-1 Scale Coaching", tag: "Coaching", rev: "$1,240", extra: "8 sessions", tagColor: "#ff7849" },
];

const memberCourses = [
  { name: "Advanced Marketing Strategy", progress: 80, total: "12 lessons", tagColor: "#4d8eff" },
  { name: "AI Automation Mastery", progress: 45, total: "10 lessons", tagColor: "#7c5cfc" },
  { name: "Design Systems 101", progress: 100, total: "8 lessons", tagColor: "#4edea3" },
];

function BrowseWorkspaces() {
  const { joinWorkspace, logout } = useAuth();
  const [workspaces, setWorkspaces] = useState([]);
  const [loading, setLoading] = useState(true);
  const [joining, setJoining] = useState(null);
  const [error, setError] = useState("");

  useEffect(() => {
    fetch("http://localhost:5083/api/marketplace/workspaces")
      .then((res) => res.json())
      .then((data) => { setWorkspaces(data); setLoading(false); })
      .catch(() => setLoading(false));
  }, []);

  const handleJoin = async (slug) => {
    setJoining(slug);
    setError("");
    try {
      await joinWorkspace(slug);
    } catch (err) {
      setError(err.message);
      setJoining(null);
    }
  };

  return (
    <div className="dashboard">
      <aside className="sidebar">
        <div className="sidebar-top">
          <div className="sidebar-brand">
            <div className="brand-icon">C</div>
            <div>
              <div className="brand-name">Creator OS</div>
              <div className="brand-sub">Browse Workspaces</div>
            </div>
          </div>

          <nav className="sidebar-nav">
            {browseSidebar.map((item) => (
              <a key={item.label} className={`sidebar-link ${item.active ? "active" : ""}`} href="#" onClick={(e) => e.preventDefault()}>
                <span className="material-symbols-outlined">{item.icon}</span>
                {item.label}
              </a>
            ))}
          </nav>
        </div>

        <div className="sidebar-bottom">
          <a className="sidebar-link" href="#" onClick={(e) => e.preventDefault()}>
            <span className="material-symbols-outlined">help</span>
            Help Center
          </a>
          <div className="sidebar-tenant" onClick={logout} style={{ cursor: "pointer" }}>
            <div className="tenant-avatar">U</div>
            <div>
              <div className="tenant-name">Browse Mode</div>
              <div className="tenant-plan" style={{ color: "var(--text-muted)" }}>Click to sign out</div>
            </div>
          </div>
        </div>
      </aside>

      <div className="main-area">
        <header className="topbar">
          <div className="search-bar">
            <span className="material-symbols-outlined">search</span>
            <input type="text" placeholder="Search workspaces..." />
          </div>
          <div className="topbar-right">
            <button className="topbar-icon">
              <span className="material-symbols-outlined">notifications</span>
            </button>
          </div>
        </header>

        <main className="content">
          <div className="greeting-section">
            <div>
              <h1 className="greeting-title">Discover Workspaces 🌐</h1>
              <p className="greeting-date">Browse and join creator workspaces to access courses, memberships, and more</p>
            </div>
          </div>

          {error && <div className="error-message" style={{ marginBottom: "1rem" }}>{error}</div>}

          {loading ? (
            <p style={{ color: "var(--text-muted)" }}>Loading workspaces...</p>
          ) : workspaces.length === 0 ? (
            <div className="activity-card" style={{ textAlign: "center", padding: "3rem" }}>
              <span className="material-symbols-outlined" style={{ fontSize: "48px", color: "var(--text-muted)" }}>storefront</span>
              <p style={{ color: "var(--text-muted)", marginTop: "1rem" }}>No workspaces available yet</p>
            </div>
          ) : (
            <div className="workspace-grid">
              {workspaces.map((ws) => (
                <div key={ws.id} className="workspace-card">
                  <div className="workspace-card-header">
                    <div className="workspace-avatar">{ws.name[0].toUpperCase()}</div>
                    <div>
                      <div className="workspace-name">{ws.name}</div>
                      <div className="workspace-meta">{ws.memberCount} member{ws.memberCount !== 1 ? "s" : ""}</div>
                    </div>
                    <span className={`workspace-plan plan-${ws.plan}`}>{ws.plan}</span>
                  </div>
                  <button
                    className="btn-primary"
                    style={{ marginTop: "1rem" }}
                    onClick={() => handleJoin(ws.slug)}
                    disabled={joining === ws.slug}
                  >
                    {joining === ws.slug ? "Joining..." : "Join Workspace"}
                  </button>
                </div>
              ))}
            </div>
          )}
        </main>
      </div>
    </div>
  );
}

export default function Dashboard() {
  const { user, currentTenant, logout } = useAuth();
  const [currentPage, setCurrentPage] = useState("dashboard");
  const [analytics, setAnalytics] = useState(null);
  const [analyticsLoading, setAnalyticsLoading] = useState(true);

  useEffect(() => {
    if (!currentTenant) return;
    setAnalyticsLoading(true);
    analyticsApi.overview()
      .then((data) => { setAnalytics(data); setAnalyticsLoading(false); })
      .catch(() => setAnalyticsLoading(false));
  }, [currentTenant]);

  if (!currentTenant) {
    return <BrowseWorkspaces />;
  }

  const isCreator = currentTenant.role === "owner";
  const sidebarItems = isCreator ? creatorSidebar : memberSidebar;
  const quickTools = isCreator ? creatorQuickTools : memberQuickTools;
  const recentActivity = isCreator ? recentActivityCreator : recentActivityMember;

  const hour = new Date().getHours();
  const greeting = hour < 12 ? "Good morning" : hour < 18 ? "Good afternoon" : "Good evening";
  const firstName = user?.DisplayName?.split(" ")[0] || user?.email?.split("@")[0] || "Creator";

  return (
    <div className="dashboard">
      {/* Sidebar */}
      <aside className="sidebar">
        <div className="sidebar-top">
          <div className="sidebar-brand">
            <div className="brand-icon">C</div>
            <div>
              <div className="brand-name">Creator OS</div>
              <div className="brand-sub">{currentTenant?.name || "Workspace"}</div>
            </div>
          </div>

          {isCreator && (
            <button className="new-project-btn">
              <span className="material-symbols-outlined">add</span>
              New Project
            </button>
          )}

          <nav className="sidebar-nav">
            {sidebarItems.map((item) => {
              const pageKey = item.label.toLowerCase().replace(/\s+/g, "-").replace("my-", "");
              const isActive = currentPage === pageKey || (currentPage === "dashboard" && item.active);
              return (
                <a
                  key={item.label}
                  className={`sidebar-link ${isActive ? "active" : ""}`}
                  href="#"
                  onClick={(e) => { e.preventDefault(); setCurrentPage(pageKey); }}
                >
                  <span className="material-symbols-outlined">{item.icon}</span>
                  {item.label}
                </a>
              );
            })}
          </nav>
        </div>

        <div className="sidebar-bottom">
          <a className="sidebar-link" href="#" onClick={(e) => e.preventDefault()}>
            <span className="material-symbols-outlined">description</span>
            Documentation
          </a>
          <a className="sidebar-link" href="#" onClick={(e) => e.preventDefault()}>
            <span className="material-symbols-outlined">help</span>
            Help Center
          </a>
          <div className="sidebar-tenant">
            <div className="tenant-avatar">
              {(currentTenant?.name || "W")[0].toUpperCase()}
            </div>
            <div>
              <div className="tenant-name">{currentTenant?.name || "Workspace"}</div>
              <div className="tenant-plan">{isCreator ? "Creator" : "Member"}</div>
            </div>
            <span className="material-symbols-outlined tenant-expand">expand_more</span>
          </div>
        </div>
      </aside>

      {/* Main Content */}
      <div className="main-area">
        {/* Top Bar */}
        <header className="topbar">
          <div className="search-bar">
            <span className="material-symbols-outlined">search</span>
            <input type="text" placeholder="Search anything (⌘K)..." />
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
              <div className="user-avatar">
                {user?.DisplayName?.[0] || user?.email?.[0] || "U"}
              </div>
              <div>
                <div className="user-name">{user?.DisplayName || "User"}</div>
                <div className="user-role">{isCreator ? "Creator & Owner" : "Member"}</div>
              </div>
            </div>
          </div>
        </header>

        {/* Content */}
        <main className="content">
          {currentPage === "commerce" ? (
            <Products />
          ) : (<>
          {/* Greeting */}
          <div className="greeting-section">
            <div>
              <h1 className="greeting-title">{greeting}, {firstName}! 👋</h1>
              <p className="greeting-date">
                {new Date().toLocaleDateString("en-US", { weekday: "long", month: "long", day: "numeric", year: "numeric" })}
              </p>
              {isCreator && <p className="greeting-growth">📈 Your platform is growing 18.4% faster than last week</p>}
            </div>
            {isCreator && (
              <div className="greeting-actions">
                <button className="btn-outline">
                  <span className="material-symbols-outlined">download</span>
                  Export Report
                </button>
                <button className="btn-outline">
                  <span className="material-symbols-outlined">web</span>
                  Create Website
                </button>
                <button className="btn-primary-dash">
                  <span className="material-symbols-outlined">add</span>
                  New Project
                </button>
              </div>
            )}
          </div>

          {/* Stats Cards — Creator */}
          {isCreator && (
            <div className="stats-grid">
              <div className="stat-card">
                <div className="stat-header">
                  <span className="stat-label">Total Revenue</span>
                  <span className="stat-icon blue">
                    <span className="material-symbols-outlined">account_balance_wallet</span>
                  </span>
                </div>
                <div className="stat-value">{analytics?.totalRevenueFormatted ?? "$0.00"}</div>
                <div className="stat-sub">{analytics?.totalOrders ?? 0} total orders</div>
              </div>
              <div className="stat-card">
                <div className="stat-header">
                  <span className="stat-label">Total Orders</span>
                  <span className="stat-icon purple">
                    <span className="material-symbols-outlined">shopping_bag</span>
                  </span>
                </div>
                <div className="stat-value">{analytics?.totalOrders ?? 0}</div>
                <div className="stat-sub">{analytics?.totalCustomers ?? 0} customers</div>
              </div>
              <div className="stat-card">
                <div className="stat-header">
                  <span className="stat-label">Products</span>
                  <span className="stat-icon green">
                    <span className="material-symbols-outlined">inventory_2</span>
                  </span>
                </div>
                <div className="stat-value">{analytics?.totalProducts ?? 0}</div>
                <div className="stat-sub">Active products</div>
              </div>
              <div className="stat-card">
                <div className="stat-header">
                  <span className="stat-label">Apps</span>
                  <span className="stat-icon orange">
                    <span className="material-symbols-outlined">widgets</span>
                  </span>
                </div>
                <div className="stat-value">{analytics?.totalApps ?? 0}</div>
                <div className="stat-sub">Built applications</div>
              </div>
            </div>
          )}

          {/* Stats Cards — Member */}
          {!isCreator && (
            <div className="stats-grid" style={{ gridTemplateColumns: "repeat(3, 1fr)" }}>
              <div className="stat-card">
                <div className="stat-header">
                  <span className="stat-label">Courses Enrolled</span>
                  <span className="stat-icon blue">
                    <span className="material-symbols-outlined">school</span>
                  </span>
                </div>
                <div className="stat-value">3</div>
                <div className="stat-sub">1 completed</div>
              </div>
              <div className="stat-card">
                <div className="stat-header">
                  <span className="stat-label">Active Memberships</span>
                  <span className="stat-icon purple">
                    <span className="material-symbols-outlined">card_membership</span>
                  </span>
                </div>
                <div className="stat-value">1</div>
                <div className="stat-sub">Pro Plan • $29/mo</div>
              </div>
              <div className="stat-card">
                <div className="stat-header">
                  <span className="stat-label">Total Spent</span>
                  <span className="stat-icon green">
                    <span className="material-symbols-outlined">receipt_long</span>
                  </span>
                </div>
                <div className="stat-value">$397</div>
                <div className="stat-sub">Across 4 orders</div>
              </div>
            </div>
          )}

          {/* Chart — Creator only */}
          {isCreator && (
            <div className="two-col">
              <div className="chart-card">
                <div className="chart-header">
                  <div>
                    <h3>Revenue Growth & Sales Volume</h3>
                    <p>Aggregated multi-channel subscription and digital product revenues</p>
                  </div>
                  <div className="chart-controls">
                    <span className="sync-badge">🔄 Real-time Sync</span>
                    <div className="chart-tabs">
                      <button>7D</button>
                      <button className="active">30D</button>
                      <button>90D</button>
                      <button>1Y</button>
                    </div>
                  </div>
                </div>
                <div className="chart-placeholder">
                  <div className="chart-line"></div>
                  <div className="chart-tooltip">
                    <div className="tooltip-date">Oct 23, 2025</div>
                    <div className="tooltip-value">$12,450.00</div>
                    <div className="tooltip-change">+24% surge in memberships</div>
                  </div>
                  <div className="chart-labels">
                    <span>Oct 01</span><span>Oct 05</span><span>Oct 10</span><span>Oct 15</span><span>Oct 20</span><span>Oct 23</span><span>Oct 24</span>
                  </div>
                </div>
              </div>

              <div className="ai-insights-card">
                <div className="ai-header">
                  <span className="ai-badge">✨ AI Insights</span>
                  <span className="ai-time">Just now</span>
                </div>
                <p className="ai-text">
                  Traffic spiked <strong>34%</strong> from YouTube referrers. Consider launching an automated flash sale on your Digital Kit.
                </p>
                <div className="ai-metrics">
                  <div>
                    <span className="ai-metric-label">Projected Lift:</span>
                    <span className="ai-metric-value green">+$1,850.00</span>
                  </div>
                  <div>
                    <span className="ai-metric-label">Recommended Discount:</span>
                    <span className="ai-metric-value">20% off for 48h</span>
                  </div>
                </div>
                <button className="btn-ai">
                  <span className="material-symbols-outlined">auto_awesome</span>
                  Apply AI Strategy
                </button>
              </div>
            </div>
          )}

          {/* Member Course Progress */}
          {!isCreator && (
            <div className="section">
              <h3 className="section-title">Continue Learning</h3>
              <div className="tools-grid">
                {memberCourses.map((c) => (
                  <div key={c.name} className="tool-card" style={{ flexDirection: "column", alignItems: "stretch", gap: "0.5rem" }}>
                    <div style={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                      <div className="tool-label">{c.name}</div>
                      <span className="product-tag" style={{ background: c.tagColor + "22", color: c.tagColor, fontSize: "0.625rem" }}>
                        {c.progress === 100 ? "Completed" : "In Progress"}
                      </span>
                    </div>
                    <div style={{ fontSize: "0.75rem", color: "var(--text-muted)" }}>{c.total}</div>
                    <div className="progress-bar">
                      <div className="progress-fill" style={{ width: `${c.progress}%`, background: c.tagColor }}></div>
                    </div>
                  </div>
                ))}
              </div>
            </div>
          )}

          {/* Quick Studio Tools */}
          <div className="section">
            <h3 className="section-title">{isCreator ? "Quick Studio Tools" : "Quick Actions"}</h3>
            <div className="tools-grid">
              {quickTools.map((tool) => (
                <a key={tool.label} className="tool-card" href="#" onClick={(e) => e.preventDefault()}>
                  <span className="material-symbols-outlined tool-icon">{tool.icon}</span>
                  <div>
                    <div className="tool-label">{tool.label}</div>
                    <div className="tool-desc">{tool.desc}</div>
                  </div>
                </a>
              ))}
            </div>
          </div>

          {/* Recent Activity + Side panel */}
          <div className="three-col">
            <div className="activity-card">
              <div className="card-header">
                <h3>Recent Activity</h3>
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
              {isCreator && (
                <>
                  <div className="products-card">
                    <div className="card-header">
                      <h3>Top Products</h3>
                      <a href="#" onClick={(e) => e.preventDefault()}>View catalog</a>
                    </div>
                    <div className="products-list">
                      {topProducts.map((p, i) => (
                        <div key={i} className="product-item">
                          <div className="product-info">
                            <div className="product-name">{p.name}</div>
                            <span className="product-tag" style={{ background: p.tagColor + "22", color: p.tagColor }}>{p.tag}</span>
                          </div>
                          <div className="product-right">
                            <span className="product-rev">{p.rev} rev</span>
                            <span className="product-extra">{p.extra}</span>
                          </div>
                        </div>
                      ))}
                    </div>
                  </div>

                  <div className="limits-card">
                    <div className="card-header">
                      <h3>Resource Limits</h3>
                      <span className="status-dot"></span>
                    </div>
                    <div className="limits-list">
                      <div className="limit-item">
                        <span className="material-symbols-outlined">wifi</span>
                        <span className="limit-label">CDN Bandwidth</span>
                        <span className="limit-value">142.8 GB / 500 GB</span>
                      </div>
                      <div className="limit-item">
                        <span className="material-symbols-outlined">auto_awesome</span>
                        <span className="limit-label">AI Generation Budget</span>
                        <span className="limit-value">1,200 / 10,000</span>
                      </div>
                      <div className="limit-item">
                        <span className="material-symbols-outlined">video_library</span>
                        <span className="limit-label">Video Vault Storage</span>
                        <span className="limit-value">68.2 GB / 100 GB</span>
                      </div>
                    </div>
                    <div className="limits-footer">
                      <span>Resets in 7 days</span>
                      <a href="#" onClick={(e) => e.preventDefault()}>Upgrade Tier →</a>
                    </div>
                  </div>
                </>
              )}

              {!isCreator && (
                <div className="products-card">
                  <div className="card-header">
                    <h3>Active Subscriptions</h3>
                    <a href="#" onClick={(e) => e.preventDefault()}>Manage</a>
                  </div>
                  <div className="products-list">
                    <div className="product-item">
                      <div className="product-info">
                        <div className="product-name">Pro Membership</div>
                        <span className="product-tag" style={{ background: "#4edea322", color: "#4edea3" }}>Active</span>
                      </div>
                      <div className="product-right">
                        <span className="product-rev">$29/mo</span>
                        <span className="product-extra">Renews Oct 24</span>
                      </div>
                    </div>
                  </div>
                </div>
              )}
            </div>
            </div>
          </>)}
        </main>
      </div>
    </div>
  );
}
