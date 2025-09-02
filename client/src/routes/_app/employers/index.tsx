import { createFileRoute } from "@tanstack/react-router";
import EmployerDashboard from "../../../features/employers/components/EmployerDashboard";
import Header from "../../../features/landingPages/components/Header";
import { ProtectedRoutes } from "../../../features/auth/components/ProtectedRoutes";

export const Route = createFileRoute("/_app/employers/")({
  component: () => {
    return (
      <ProtectedRoutes allowedRoles={["Employer"]}>
        <EmployerPage />
      </ProtectedRoutes>
    );
  },
});

function EmployerPage() {
  return (
    <>
      <Header />
      <EmployerDashboard />
    </>
  );
}
