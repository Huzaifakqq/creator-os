import { useState, useEffect } from "react";
import { products as productsApi } from "../services/api";
import "../styles/dashboard.css";
import "../styles/products.css";

const productTypeIcons = {
  digital: "download",
  course: "school",
  coaching: "fitness_center",
  subscription: "card_membership",
  physical: "local_shipping",
};

const productTypeColors = {
  digital: "#4d8eff",
  course: "#7c5cfc",
  coaching: "#ff7849",
  subscription: "#4edea3",
  physical: "#ffb04d",
};

export default function Products() {
  const [productList, setProductList] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState("");
  const [showCreate, setShowCreate] = useState(false);
  const [creating, setCreating] = useState(false);
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [productType, setProductType] = useState("digital");
  const [price, setPrice] = useState("");

  const fetchProducts = async () => {
    try {
      const data = await productsApi.getAll();
      setProductList(data);
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => { fetchProducts(); }, []);

  const handleCreate = async (e) => {
    e.preventDefault();
    setCreating(true);
    setError("");
    try {
      const priceCents = Math.round(parseFloat(price) * 100);
      await productsApi.create({ name, description, productType, priceCents });
      setShowCreate(false);
      setName(""); setDescription(""); setProductType("digital"); setPrice("");
      fetchProducts();
    } catch (err) {
      setError(err.message);
    } finally {
      setCreating(false);
    }
  };

  const handleDelete = async (id) => {
    if (!window.confirm("Delete this product?")) return;
    try {
      await productsApi.delete(id);
      fetchProducts();
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <div className="products-page">
      <div className="page-header">
        <div>
          <h2 className="page-title">Products</h2>
          <p className="page-subtitle">Manage your digital products, courses, and services</p>
        </div>
        <button className="btn-primary-dash" onClick={() => setShowCreate(true)}>
          <span className="material-symbols-outlined">add</span>
          Add Product
        </button>
      </div>

      {error && <div className="error-message" style={{ marginBottom: "1rem" }}>{error}</div>}

      {showCreate && (
        <div className="create-form-card">
          <div className="create-form-header">
            <h3>Create New Product</h3>
            <button className="btn-close" onClick={() => setShowCreate(false)}>
              <span className="material-symbols-outlined">close</span>
            </button>
          </div>
          <form onSubmit={handleCreate} className="create-form">
            <div className="form-row-2">
              <div className="form-group">
                <label>Product name</label>
                <input type="text" placeholder="e.g. Social Media Templates" value={name} onChange={(e) => setName(e.target.value)} required />
              </div>
              <div className="form-group">
                <label>Price ($)</label>
                <input type="number" step="0.01" min="0" placeholder="29.99" value={price} onChange={(e) => setPrice(e.target.value)} required />
              </div>
            </div>
            <div className="form-group">
              <label>Description</label>
              <textarea placeholder="Describe your product..." value={description} onChange={(e) => setDescription(e.target.value)} rows={3} />
            </div>
            <div className="form-group">
              <label>Type</label>
              <div className="type-selector">
                {["digital", "course", "coaching", "subscription", "physical"].map((t) => (
                  <button key={t} type="button" className={`type-btn ${productType === t ? "active" : ""}`} onClick={() => setProductType(t)}>
                    <span className="material-symbols-outlined" style={{ color: productTypeColors[t] }}>{productTypeIcons[t]}</span>
                    {t.charAt(0).toUpperCase() + t.slice(1)}
                  </button>
                ))}
              </div>
            </div>
            <div className="form-actions">
              <button type="button" className="btn-cancel" onClick={() => setShowCreate(false)}>Cancel</button>
              <button type="submit" className="btn-primary-dash" disabled={creating}>{creating ? "Creating..." : "Create Product"}</button>
            </div>
          </form>
        </div>
      )}

      {loading ? (
        <p style={{ color: "var(--text-muted)" }}>Loading products...</p>
      ) : productList.length === 0 ? (
        <div className="empty-state">
          <span className="material-symbols-outlined">inventory_2</span>
          <h3>No products yet</h3>
          <p>Create your first product to start selling</p>
          <button className="btn-primary-dash" onClick={() => setShowCreate(true)}>
            <span className="material-symbols-outlined">add</span> Create Product
          </button>
        </div>
      ) : (
        <div className="products-grid">
          {productList.map((p) => (
            <div key={p.id} className="product-card">
              <div className="product-card-icon" style={{ background: (productTypeColors[p.productType] || "#4d8eff") + "22" }}>
                <span className="material-symbols-outlined" style={{ color: productTypeColors[p.productType] || "#4d8eff" }}>
                  {productTypeIcons[p.productType] || "inventory_2"}
                </span>
              </div>
              <div className="product-card-info">
                <h4>{p.name}</h4>
                <span className="product-type-badge" style={{ color: productTypeColors[p.productType] || "#4d8eff" }}>
                  {p.productType}
                </span>
              </div>
              <div className="product-card-price">
                ${(p.priceCents / 100).toFixed(2)}
              </div>
              <div className="product-card-actions">
                <span className={`publish-badge ${p.isPublished ? "published" : "draft"}`}>
                  {p.isPublished ? "Published" : "Draft"}
                </span>
                <button className="btn-icon" onClick={() => handleDelete(p.id)} title="Delete">
                  <span className="material-symbols-outlined">delete</span>
                </button>
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
}
