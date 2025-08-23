import { Outlet, createRootRoute } from "@tanstack/react-router";
import Header from "../features/landingPages/components/Header";
import Hero from "../features/landingPages/components/Hero";

export const Route = createRootRoute({
  component: () => (
    <>
      <Header />
      <Hero />
      <Outlet />
    </>
  ),
});
