import { createFileRoute } from "@tanstack/react-router";
import JobPostingForm from "../../../features/jobSeekers/components/JobPostingForm";
import Header from "../../../features/landingPages/components/Header";
import { ProtectedRoutes } from "../../../features/auth/components/ProtectedRoutes";

export const Route = createFileRoute("/_app/jobSeekers/")({
  component: () => {
    return (
      <ProtectedRoutes allowedRoles={["JobSeeker"]}>
        <JobSeekerPage />
      </ProtectedRoutes>
    );
  },
});

function JobSeekerPage() {
  return (
    <>
      <Header />
      <JobPostingForm />
    </>
  );
}
