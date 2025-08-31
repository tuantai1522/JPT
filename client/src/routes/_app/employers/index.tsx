import { createFileRoute } from "@tanstack/react-router";
import EmployerDashboard from "../../../features/employer/components/EmployerDashboard";
import Header from "../../../features/landingPages/components/Header";

export const Route = createFileRoute("/_app/employers/")({
  component: EmployerPage,
});

function EmployerPage() {
  return (
    <>
      <Header />
      <EmployerDashboard />
    </>
  );
}
