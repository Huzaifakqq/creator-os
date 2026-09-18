---
name: Creator OS
colors:
  surface: '#131318'
  surface-dim: '#131318'
  surface-bright: '#39383e'
  surface-container-lowest: '#0e0e13'
  surface-container-low: '#1b1b20'
  surface-container: '#1f1f25'
  surface-container-high: '#2a292f'
  surface-container-highest: '#35343a'
  on-surface: '#e4e1e9'
  on-surface-variant: '#c2c6d6'
  inverse-surface: '#e4e1e9'
  inverse-on-surface: '#303036'
  outline: '#8c909f'
  outline-variant: '#424754'
  surface-tint: '#adc6ff'
  primary: '#adc6ff'
  on-primary: '#002e6a'
  primary-container: '#4d8eff'
  on-primary-container: '#00285d'
  inverse-primary: '#005ac2'
  secondary: '#d0bcff'
  on-secondary: '#3c0091'
  secondary-container: '#571bc1'
  on-secondary-container: '#c4abff'
  tertiary: '#4edea3'
  on-tertiary: '#003824'
  tertiary-container: '#00a572'
  on-tertiary-container: '#00311f'
  error: '#ffb4ab'
  on-error: '#690005'
  error-container: '#93000a'
  on-error-container: '#ffdad6'
  primary-fixed: '#d8e2ff'
  primary-fixed-dim: '#adc6ff'
  on-primary-fixed: '#001a42'
  on-primary-fixed-variant: '#004395'
  secondary-fixed: '#e9ddff'
  secondary-fixed-dim: '#d0bcff'
  on-secondary-fixed: '#23005c'
  on-secondary-fixed-variant: '#5516be'
  tertiary-fixed: '#6ffbbe'
  tertiary-fixed-dim: '#4edea3'
  on-tertiary-fixed: '#002113'
  on-tertiary-fixed-variant: '#005236'
  background: '#131318'
  on-background: '#e4e1e9'
  surface-variant: '#35343a'
typography:
  display-lg:
    fontFamily: Inter
    fontSize: 48px
    fontWeight: '700'
    lineHeight: 56px
    letterSpacing: -0.02em
  display-lg-mobile:
    fontFamily: Inter
    fontSize: 32px
    fontWeight: '700'
    lineHeight: 40px
    letterSpacing: -0.02em
  headline-xl:
    fontFamily: Inter
    fontSize: 36px
    fontWeight: '600'
    lineHeight: 44px
    letterSpacing: -0.02em
  headline-xl-mobile:
    fontFamily: Inter
    fontSize: 26px
    fontWeight: '600'
    lineHeight: 34px
    letterSpacing: -0.015em
  headline-lg:
    fontFamily: Inter
    fontSize: 28px
    fontWeight: '600'
    lineHeight: 36px
    letterSpacing: -0.015em
  headline-md:
    fontFamily: Inter
    fontSize: 22px
    fontWeight: '600'
    lineHeight: 30px
    letterSpacing: -0.01em
  headline-sm:
    fontFamily: Inter
    fontSize: 18px
    fontWeight: '600'
    lineHeight: 26px
    letterSpacing: -0.005em
  body-lg:
    fontFamily: Inter
    fontSize: 16px
    fontWeight: '400'
    lineHeight: 24px
    letterSpacing: 0em
  body-md:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '400'
    lineHeight: 20px
    letterSpacing: 0em
  body-sm:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '400'
    lineHeight: 18px
    letterSpacing: 0.01em
  label-lg:
    fontFamily: Inter
    fontSize: 14px
    fontWeight: '500'
    lineHeight: 20px
    letterSpacing: 0.01em
  label-md:
    fontFamily: Inter
    fontSize: 12px
    fontWeight: '500'
    lineHeight: 16px
    letterSpacing: 0.02em
  label-sm:
    fontFamily: Inter
    fontSize: 11px
    fontWeight: '600'
    lineHeight: 14px
    letterSpacing: 0.04em
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  gutter: 1.5rem
  gutter-sm: 1rem
  margin: 2rem
  margin-sm: 1rem
  space-xs: 0.25rem
  space-sm: 0.5rem
  space-md: 1rem
  space-lg: 1.5rem
  space-xl: 2rem
---

## Brand & Style

This design system delivers a high-performance, studio-grade environment tailored for modern digital creators, media entrepreneurs, and multi-tenant operational teams. The visual direction balances the precision of an IDE with the visual allure of modern creative software. 

The aesthetic merges **minimalist dark mode architecture** with **curated glassmorphic accents and subtle ambient luminescence**. The goal is to minimize cognitive friction, spotlight user media and analytical assets, and evoke a sense of focused power, deep focus, and enterprise-grade reliability.

### Design Principles
- **Atmospheric Depth:** Rely on disciplined surface layering from deep obsidian to elevated obsidian tones rather than harsh dividers or heavy skeumorphic textures.
- **Controlled Luminescence:** Restrict radiant gradients and neon accents to primary calls-to-action, active workspaces, and key state transitions. Ambient glow must remain subtle and background-bound.
- **Instrument Precision:** Interface components behave like precision digital instruments: tight geometry, high text legibility, crisp 1px borders, and ultra-responsive hover cues.

## Colors

The system relies on an intentional dark palette structured around deep obsidian foundations, low-noise mid-tones, and dynamic electric accents.

### Palette Architecture
- **Base Canvas (`#0A0A0F`):** Deep obsidian. Used strictly for full-viewport application backgrounds and root frames.
- **Surface Level 1 (`#12121A`):** Standard container surface for dashboards, cards, side panels, and content modules.
- **Surface Level 2 (`#181824`):** Elevated elements, modal dialogs, flyout menus, and interactive card hover states.
- **Borders & Dividers (`#1E1E2E`):** Crisp structural lines that provide subtle spatial delineation without visual noise.
- **Primary Accent Gradient:** `linear-gradient(135deg, #3B82F6 0%, #8B5CF6 100%)`. Reserved for high-value conversions, active navigation indicators, key progress states, and prominent interaction elements.
- **Text Primary (`#FFFFFF`):** High contrast for headlines, values, active tabs, and primary controls.
- **Text Secondary (`#9CA3AF`):** Balanced neutral gray for labels, secondary metadata, body copy, and iconography.
- **Semantic Accents:**
  - **Success (`#10B981`):** Monetization tracking, publish confirms, live stream healthy status.
  - **Warning (`#F59E0B`):** System alerts, quota usage thresholds, draft notices.
  - **Error (`#EF4444`):** Failed network requests, ingestion conflicts, deletion actions.

## Typography

The typographic hierarchy is built on a single, highly refined sans-serif engine: **Inter**. By utilizing consistent geometric proportions across weights, the interface maintains razor-sharp clarity across complex multi-column dashboards, density-heavy tables, and contextual inspector sidebars.

### Hierarchy & Treatment
- **Display & Large Headlines:** Styled with negative tracking (`-0.02em` to `-0.01em`) to create a cohesive editorial feel in analytics highlights and onboarding modules.
- **Data & Numeric Readouts:** Set with tabular numeral settings (`font-variant-numeric: tabular-nums`) across metrics, revenue counters, and real-time logs to prevent layout jitter.
- **Micro-Labels & Tags:** Uppercase usage is strictly reserved for `label-sm` metadata tags, badges, and system state pills, accompanied by expanded tracking (`0.04em`) to ensure legibility.

## Layout & Spacing

The layout is built on a responsive 12-column fluid grid system paired with a strict 4px/8px layout rhythm.

### Grid & Breakpoints
- **Desktop (1280px and above):** 12-column fluid grid with fixed collapsible navigation drawers (80px collapsed, 260px expanded). Gutters are `1.5rem` (`24px`) and margins are `2rem` (`32px`).
- **Tablet (768px – 1279px):** 8-column layout. Navigation transitions to an off-canvas drawer. Gutters scale down to `1rem` (`16px`); outer margins scale to `1.5rem` (`24px`).
- **Mobile (Below 768px):** 4-column single-stack layout. Navigation shifts to a persistent bottom utility bar or header hamburger sheet. Outer margins compress to `1rem` (`16px`) with `1rem` (`16px`) gutters.

### Structure & Layout Density
- Content modules employ internal vertical stacks governed by `space-sm` (`8px`) for clustered field groups and `space-md` (`16px`) for primary card section breaks.
- Full viewport setups utilize sticky utility headers (56px fixed height) with overflow-y isolated scrolling inside distinct workspace canvas partitions.

## Elevation & Depth

Visual hierarchy uses a hybrid strategy combining **tonal surface layering**, **glassmorphism**, and **ambient colored luminescence**.

### Depth Layers
- **Level 0 (Canvas Base):** Solid `#0A0A0F`. Non-elevated, absorbs background light.
- **Level 1 (Card & Module Foundation):** Solid `#12121A` paired with a 1px border of `#1E1E2E`. Zero standard drop shadow to preserve contrast purity.
- **Level 2 (Popovers, Dropdowns, Hovered Modules):** Surface `#181824`, border `#2A2A3E`, with an ambient drop shadow: `0 12px 32px -4px rgba(0, 0, 0, 0.6)`.
- **Level 3 (Modals & Command Palettes):** Surface `#181824` with a high-strength backdrop filter (`backdrop-filter: blur(16px)`), framed with a crisp border of `rgba(255, 255, 255, 0.08)`. Shadow: `0 24px 64px -12px rgba(0, 0, 0, 0.85)`.

### Ambient Glows & Glass Details
- **Accent Glows:** Elements carrying the primary accent gradient receive a subtle ambient back-glow: `box-shadow: 0 0 24px -4px rgba(59, 130, 246, 0.35)`.
- **Backdrop Overlays:** Full-screen modal backdrops use `rgba(10, 10, 15, 0.75)` combined with `backdrop-filter: blur(8px)`.

## Shapes

The interface embraces a balanced, ergonomic radius hierarchy where corner radii scale progressively according to component volume and interactive context.

### Radius Scale Implementation
- **Cards & Primary Modules:** Fixed `12px` border radius (`rounded-md`). Creates a soft, modern enclosure for content while remaining structured.
- **Buttons & Interactive Badges:** Fixed `8px` border radius (`rounded-sm`). Delivers a tactile, clickable target.
- **Input Fields & Small Controls:** Fixed `6px` border radius. Ensures compact input density, crisp alignment in multi-field forms, and clean edge contact with inline icons.
- **Pills, Badges & Status Indicators:** `9999px` (Full pill radius). Used exclusively for tags, live presence status indicators, and floating filter pills.

## Components

### Buttons
- **Primary:** Background is `linear-gradient(135deg, #3B82F6 0%, #8B5CF6 100%)`, text `#FFFFFF`, radius `8px`, height `40px` (desktop) / `44px` (mobile). Hover state applies subtle filter brightness (`filter: brightness(1.08)`) and ambient glow `0 0 16px rgba(59, 130, 246, 0.4)`. Active state scales to `0.98`.
- **Secondary:** Background `#181824`, border `1px solid #1E1E2E`, text `#FFFFFF`. Hover state adjusts border to `#3B82F6` and background to `#1E1E2E`.
- **Ghost / Tertiary:** Background transparent, text `#9CA3AF`. Hover state turns text `#FFFFFF` with background `rgba(255, 255, 255, 0.05)`.

### Input Fields
- Background `#12121A`, border `1px solid #1E1E2E`, text `#FFFFFF`, placeholder `#9CA3AF` at 60% opacity. Radius `6px`, height `38px`, padding `0 12px`.
- Focus state eliminates standard browser outlines and applies `border-color: #3B82F6` alongside a subtle glow ring: `box-shadow: 0 0 0 1px #3B82F6, 0 0 12px rgba(59, 130, 246, 0.25)`.
- Error state switches border to `#EF4444` with a matching red focus glow.

### Cards & Content Modules
- Background `#12121A`, border `1px solid #1E1E2E`, radius `12px`, padding `space-lg` (`24px`).
- Interactive/clickable cards transition border color to `rgba(139, 92, 246, 0.4)` on hover, with a subtle surface lift to `#181824`.

### Checkboxes & Radio Buttons
- Base state: Width/height `18px`, background `#12121A`, border `1px solid #1E1E2E`. Checkboxes have `4px` radius; radio buttons have `50%` radius.
- Selected state: Background `linear-gradient(135deg, #3B82F6 0%, #8B5CF6 100%)`, border color transparent, check/dot icon `#FFFFFF`.

### Chips & Badges
- Pill-shaped (`rounded-full`), height `24px`, padding `0 10px`, font size `12px` (`label-md`).
- Contextual variant: Background `rgba(30, 30, 46, 0.6)`, border `1px solid #1E1E2E`, text `#9CA3AF`.
- Active variant: Background `rgba(59, 130, 246, 0.15)`, border `1px solid rgba(59, 130, 246, 0.4)`, text `#3B82F6`.

### Lists & Tables
- Table rows sit on transparent or alternating `#12121A` backgrounds with bottom borders of `1px solid #1E1E2E`.
- Row hover triggers background change to `#181824` with a smooth 150ms ease-out transition.

### Contextual Components
- **Tenant Switcher:** Compact header menu button with a dual-tone badge indicating the active tenant space, enclosed in a `6px` rounded container with quick-search dropdown functionality.
- **Metric Micro-Charts:** Sparklines housed inside metric cards utilizing the `#3B82F6` to `#8B5CF6` gradient stroke with transparent fill falloffs.