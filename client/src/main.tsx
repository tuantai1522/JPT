import { StrictMode } from "react";
import { RouterProvider } from "@tanstack/react-router";
// Import the generated route tree
import ReactDOM from "react-dom/client";
import "./index.css";
import { createRouter } from "./router";

const router = createRouter();

// Render the app
const rootElement = document.getElementById("root")!;
if (!rootElement.innerHTML) {
  const root = ReactDOM.createRoot(rootElement);
  root.render(
    <StrictMode>
      <RouterProvider router={router} />
    </StrictMode>
  );
}
