import { createFileRoute } from "@tanstack/react-router";
import Hero from "../features/landingPages/components/Hero";
import Header from "../features/landingPages/components/Header";

export const Route = createFileRoute("/")({
  component: HomePage,
});

function HomePage() {
  return (
    <>
      <Header />
      <Hero />
    </>
  );
}
