import { useState } from "react";
import { useAuth } from "../context/AuthContext";

export default function Register({ onSwitch }) {
  const { register } = useAuth();
  const [step, setStep] = useState("choose"); // choose | creator | member
  const [displayName, setDisplayName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);

  const [tenantName, setTenantName] = useState("");
  const [inviteCode, setInviteCode] = useState("");

  const handleCreatorSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      await register(email, password, displayName, "creator", tenantName);
      console.log("[Register] Creator account created");
      setSuccess(true);
      setTimeout(() => onSwitch("login"), 2000);
    } catch (err) {
      console.error("[Register] Error:", err.message);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  const handleMemberSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      await register(email, password, displayName, "member", null);
      console.log("[Register] Member account created");
      setSuccess(true);
      setTimeout(() => onSwitch("login"), 2000);
    } catch (err) {
      console.error("[Register] Error:", err.message);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  if (success) {
    return (
      <div className="login-page">
        <div className="login-bg">
          <div className="orb orb-1"></div>
          <div className="orb orb-2"></div>
          <div className="orb orb-3"></div>
          <div className="grid-overlay"></div>
        </div>
        <main className="login-main">
          <div className="auth-card" style={{ textAlign: "center" }}>
            <div className="success-message">
              <span className="material-symbols-outlined">check_circle</span>
              Account created! Redirecting to sign in...
            </div>
          </div>
        </main>
      </div>
    );
  }

  // Step 1: Choose role
  if (step === "choose") {
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
            <div className="login-badge">
              <span className="badge-dot"></span>
              Enterprise Node — v2.4 Active
            </div>
            <h1 className="login-title">Creator OS</h1>
            <p className="login-subtitle">
              The all-in-one operating system for modern creators
            </p>
          </div>

          <section className="auth-card">
            <div className="auth-card-header">
              <h2 className="auth-card-title">Create your account</h2>
              <p className="auth-card-subtitle">
                How do you want to use Creator OS?
              </p>
            </div>

            <div className="role-selector">
              <button className="role-card" onClick={() => setStep("creator")}>
                <span className="material-symbols-outlined role-icon creator-icon">storefront</span>
                <div className="role-info">
                  <div className="role-label">Create &amp; Sell</div>
                  <div className="role-desc">Build a workspace, sell products, courses, and deploy AI agents</div>
                </div>
                <span className="material-symbols-outlined role-arrow">arrow_forward</span>
              </button>

              <button className="role-card" onClick={() => setStep("member")}>
                <span className="material-symbols-outlined role-icon member-icon">school</span>
                <div className="role-info">
                  <div className="role-label">Join &amp; Learn</div>
                  <div className="role-desc">Access courses, memberships, and coaching from creators</div>
                </div>
                <span className="material-symbols-outlined role-arrow">arrow_forward</span>
              </button>
            </div>

            <div className="auth-footer">
              <p>
                Already have an account?{" "}
                <a href="#" onClick={(e) => { e.preventDefault(); onSwitch("login"); }}>
                  Sign in
                </a>
              </p>
            </div>
          </section>

          <div className="trust-indicators">
            <span className="material-symbols-outlined">lock</span>
            <span>Protected with 256-bit encryption</span>
            <span>•</span>
            <span className="material-symbols-outlined">verified_user</span>
            <span>SOC-2 Certified</span>
          </div>
        </main>

        <footer className="login-footer">
          <p>© 2025 Creator OS Inc. All rights reserved.</p>
          <div className="footer-links">
            <a href="#">Privacy Policy</a>
            <a href="#">Terms of Service</a>
            <a href="#">Status</a>
            <a href="#">Support</a>
          </div>
        </footer>
      </div>
    );
  }

  // Step 2a: Creator — fill details + name workspace
  if (step === "creator") {
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
            <div className="login-badge">
              <span className="badge-dot"></span>
              Enterprise Node — v2.4 Active
            </div>
            <h1 className="login-title">Creator OS</h1>
            <p className="login-subtitle">
              The all-in-one operating system for modern creators
            </p>
          </div>

          <section className="auth-card">
            <button className="back-button" onClick={() => setStep("choose")}>
              <span className="material-symbols-outlined">arrow_back</span>
              Back
            </button>

            <div className="auth-card-header">
              <h2 className="auth-card-title">Set up your workspace</h2>
              <p className="auth-card-subtitle">
                Create your account and name your workspace
              </p>
            </div>

            <form onSubmit={handleCreatorSubmit} className="auth-form">
              <div className="form-group">
                <label htmlFor="displayName">Full name</label>
                <div className="input-wrapper">
                  <span className="material-symbols-outlined input-icon">person</span>
                  <input
                    id="displayName"
                    type="text"
                    placeholder="John Doe"
                    value={displayName}
                    onChange={(e) => setDisplayName(e.target.value)}
                    required
                  />
                </div>
              </div>

              <div className="form-group">
                <label htmlFor="email">Email address</label>
                <div className="input-wrapper">
                  <span className="material-symbols-outlined input-icon">mail</span>
                  <input
                    id="email"
                    type="email"
                    placeholder="you@creator.com"
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    required
                  />
                </div>
              </div>

              <div className="form-group">
                <label htmlFor="password">Password</label>
                <div className="input-wrapper">
                  <span className="material-symbols-outlined input-icon">lock</span>
                  <input
                    id="password"
                    type={showPassword ? "text" : "password"}
                    placeholder="••••••••"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    required
                  />
                  <button
                    type="button"
                    className="toggle-password"
                    onClick={() => setShowPassword(!showPassword)}
                  >
                    <span className="material-symbols-outlined">
                      {showPassword ? "visibility_off" : "visibility"}
                    </span>
                  </button>
                </div>
              </div>

              <div className="form-group">
                <label htmlFor="tenantName">Workspace name</label>
                <div className="input-wrapper">
                  <span className="material-symbols-outlined input-icon">business</span>
                  <input
                    id="tenantName"
                    type="text"
                    placeholder="e.g. John's Academy"
                    value={tenantName}
                    onChange={(e) => setTenantName(e.target.value)}
                    required
                  />
                </div>
              </div>

              {error && <div className="error-message">{error}</div>}

              <button type="submit" className="btn-primary" disabled={loading}>
                <span>{loading ? "Creating workspace..." : "Create Workspace"}</span>
                {!loading && (
                  <span className="material-symbols-outlined">arrow_forward</span>
                )}
              </button>
            </form>

            <div className="auth-footer">
              <p>
                Already have an account?{" "}
                <a href="#" onClick={(e) => { e.preventDefault(); onSwitch("login"); }}>
                  Sign in
                </a>
              </p>
            </div>
          </section>

          <div className="trust-indicators">
            <span className="material-symbols-outlined">lock</span>
            <span>Protected with 256-bit encryption</span>
            <span>•</span>
            <span className="material-symbols-outlined">verified_user</span>
            <span>SOC-2 Certified</span>
          </div>
        </main>

        <footer className="login-footer">
          <p>© 2025 Creator OS Inc. All rights reserved.</p>
          <div className="footer-links">
            <a href="#">Privacy Policy</a>
            <a href="#">Terms of Service</a>
            <a href="#">Status</a>
            <a href="#">Support</a>
          </div>
        </footer>
      </div>
    );
  }

  // Step 2b: Member — fill details + enter invite code
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
          <div className="login-badge">
            <span className="badge-dot"></span>
            Enterprise Node — v2.4 Active
          </div>
          <h1 className="login-title">Creator OS</h1>
          <p className="login-subtitle">
            The all-in-one operating system for modern creators
          </p>
        </div>

        <section className="auth-card">
          <button className="back-button" onClick={() => setStep("choose")}>
            <span className="material-symbols-outlined">arrow_back</span>
            Back
          </button>

          <div className="auth-card-header">
            <h2 className="auth-card-title">Join a workspace</h2>
            <p className="auth-card-subtitle">
              Create your account to access courses, memberships, and more
            </p>
          </div>

          <form onSubmit={handleMemberSubmit} className="auth-form">
            <div className="form-group">
              <label htmlFor="displayName">Full name</label>
              <div className="input-wrapper">
                <span className="material-symbols-outlined input-icon">person</span>
                <input
                  id="displayName"
                  type="text"
                  placeholder="John Doe"
                  value={displayName}
                  onChange={(e) => setDisplayName(e.target.value)}
                  required
                />
              </div>
            </div>

            <div className="form-group">
              <label htmlFor="email">Email address</label>
              <div className="input-wrapper">
                <span className="material-symbols-outlined input-icon">mail</span>
                <input
                  id="email"
                  type="email"
                  placeholder="you@creator.com"
                  value={email}
                  onChange={(e) => setEmail(e.target.value)}
                  required
                />
              </div>
            </div>

            <div className="form-group">
              <label htmlFor="password">Password</label>
              <div className="input-wrapper">
                <span className="material-symbols-outlined input-icon">lock</span>
                <input
                  id="password"
                  type={showPassword ? "text" : "password"}
                  placeholder="••••••••"
                  value={password}
                  onChange={(e) => setPassword(e.target.value)}
                  required
                />
                <button
                  type="button"
                  className="toggle-password"
                  onClick={() => setShowPassword(!showPassword)}
                >
                  <span className="material-symbols-outlined">
                    {showPassword ? "visibility_off" : "visibility"}
                  </span>
                </button>
              </div>
            </div>

            <div className="form-group">
              <label htmlFor="inviteCode">Workspace invite code</label>
              <div className="input-wrapper">
                <span className="material-symbols-outlined input-icon">vpn_key</span>
                <input
                  id="inviteCode"
                  type="text"
                  placeholder="Enter invite code from creator"
                  value={inviteCode}
                  onChange={(e) => setInviteCode(e.target.value)}
                />
              </div>
            </div>

            {error && <div className="error-message">{error}</div>}

            <button type="submit" className="btn-primary" disabled={loading}>
              <span>{loading ? "Creating account..." : "Create Account"}</span>
              {!loading && (
                <span className="material-symbols-outlined">arrow_forward</span>
              )}
            </button>
          </form>

          <div className="auth-footer">
            <p>
              Already have an account?{" "}
              <a href="#" onClick={(e) => { e.preventDefault(); onSwitch("login"); }}>
                Sign in
              </a>
            </p>
          </div>
        </section>

        <div className="trust-indicators">
          <span className="material-symbols-outlined">lock</span>
          <span>Protected with 256-bit encryption</span>
          <span>•</span>
          <span className="material-symbols-outlined">verified_user</span>
          <span>SOC-2 Certified</span>
        </div>
      </main>

      <footer className="login-footer">
        <p>© 2025 Creator OS Inc. All rights reserved.</p>
        <div className="footer-links">
          <a href="#">Privacy Policy</a>
          <a href="#">Terms of Service</a>
          <a href="#">Status</a>
          <a href="#">Support</a>
        </div>
      </footer>
    </div>
  );
}
