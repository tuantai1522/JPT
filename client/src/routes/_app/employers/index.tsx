import { createFileRoute } from "@tanstack/react-router";
import EmployerDashboard from "../../../features/employers/components/EmployerDashboard";

export const Route = createFileRoute("/_app/employers/")({
  component: EmployerPage,
});

function EmployerPage() {
  return <EmployerDashboard />;
}
