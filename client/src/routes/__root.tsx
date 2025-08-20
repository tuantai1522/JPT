import { Outlet, createRootRoute } from "@tanstack/react-router";
import Header from "../features/landingPages/Header";
import Hero from "../features/landingPages/Hero";

export const Route = createRootRoute({
  component: () => (
    <>
      <Header />
      <Hero />
      <Outlet />
    </>
  ),
});
