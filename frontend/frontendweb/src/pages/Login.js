import { useState } from "react";
import { useAuth } from "../context/AuthContext";

export default function Login({ onSwitch }) {
  const { login } = useAuth();
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      await login(email, password);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-page">
      {/* Ambient background orbs */}
      <div className="login-bg">
        <div className="orb orb-1"></div>
        <div className="orb orb-2"></div>
        <div className="orb orb-3"></div>
        <div className="grid-overlay"></div>
      </div>

      <main className="login-main">
        {/* Branding header */}
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

        {/* Auth card */}
        <section className="auth-card">
          <div className="auth-card-header">
            <h2 className="auth-card-title">Welcome back</h2>
            <p className="auth-card-subtitle">
              Sign in to manage your empire, courses, and AI agents
            </p>
          </div>

          <form onSubmit={handleSubmit} className="auth-form">
            {/* Email */}
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

            {/* Password */}
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

            {/* Remember me + Forgot */}
            <div className="form-row">
              <label className="checkbox-label">
                <input type="checkbox" />
                <span>Remember me</span>
              </label>
<a href="#" className="forgot-link" onClick={(e) => { e.preventDefault(); onSwitch("forgot"); }}>Forgot password?</a>            </div>

            {/* Error */}
            {error && <div className="error-message">{error}</div>}

            {/* Submit button */}
            <button type="submit" className="btn-primary" disabled={loading}>
              <span>{loading ? "Signing in..." : "Sign In"}</span>
              {!loading && (
                <span className="material-symbols-outlined">arrow_forward</span>
              )}
            </button>
          </form>

          {/* Divider */}
          <div className="divider">
            <span>or continue with</span>
          </div>

          {/* Google button */}
          <button type="button" className="btn-google">
            <svg className="google-icon" viewBox="0 0 24 24">
              <path d="M23.745 12.27c0-.7-.06-1.4-.19-2.07H12v4.51h6.6c-.29 1.52-1.14 2.82-2.4 3.68v3.05h3.88c2.27-2.09 3.66-5.17 3.66-9.17z" fill="#4285F4"/>
              <path d="M12 24c3.24 0 5.95-1.08 7.93-2.91l-3.88-3.05c-1.08.72-2.45 1.16-4.05 1.16-3.12 0-5.77-2.1-6.72-4.93H1.26v3.15C3.25 21.36 7.33 24 12 24z" fill="#34A853"/>
              <path d="M5.28 14.27c-.25-.72-.38-1.49-.38-2.27s.13-1.55.38-2.27V6.58H1.26C.46 8.16 0 9.99 0 12s.46 3.84 1.26 5.42l4.02-3.15z" fill="#FBBC05"/>
              <path d="M12 4.75c1.77 0 3.35.61 4.6 1.8l3.42-3.42C17.95 1.19 15.24 0 12 0 7.33 0 3.25 2.64 1.26 6.58l4.02 3.15c.95-2.83 3.6-4.98 6.72-4.98z" fill="#EA4335"/>
            </svg>
            Continue with Google
          </button>

          {/* Sign up link */}
          <div className="auth-footer">
            <p>
              Don't have an account?{" "}
<a href="#" onClick={(e) => { e.preventDefault(); onSwitch("register"); }}>Sign up</a>            </p>
          </div>
        </section>

        {/* Trust indicators */}
        <div className="trust-indicators">
          <span className="material-symbols-outlined">lock</span>
          <span>Protected with 256-bit encryption</span>
          <span>•</span>
          <span className="material-symbols-outlined">verified_user</span>
          <span>SOC-2 Certified</span>
        </div>
      </main>

      {/* Footer */}
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