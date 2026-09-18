import { useState } from "react";

export default function ForgotPassword({ onSwitch }) {
  const [email, setEmail] = useState("");
  const [sent, setSent] = useState(false);
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    setLoading(true);
    try {
      const response = await fetch("http://localhost:5083/api/auth/forgot-password", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email }),
      });
      if (!response.ok) throw new Error("Request failed");
      setSent(true);
    } catch (err) {
      setError(err.message);
    } finally {
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
            <h2 className="auth-card-title">Reset your password</h2>
            <p className="auth-card-subtitle">
              Enter your email and we'll send you a reset link
            </p>
          </div>

          {sent ? (
            <div className="success-message">
              <span className="material-symbols-outlined">check_circle</span>
              <p>Check your email for a password reset link.</p>
              <button
                type="button"
                className="btn-secondary"
                onClick={() => onSwitch("login")}
              >
                Back to Sign In
              </button>
            </div>
          ) : (
            <form onSubmit={handleSubmit} className="auth-form">
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

              {error && <div className="error-message">{error}</div>}

              <button type="submit" className="btn-primary" disabled={loading}>
                <span>{loading ? "Sending..." : "Send Reset Link"}</span>
                {!loading && (
                  <span className="material-symbols-outlined">mail</span>
                )}
              </button>
            </form>
          )}

          <div className="auth-footer" style={{ marginTop: "1.5rem" }}>
            <p>
              Remember your password?{" "}
              <a href="#" onClick={(e) => { e.preventDefault(); onSwitch("login"); }}>
                Sign in
              </a>
            </p>
          </div>
        </section>
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