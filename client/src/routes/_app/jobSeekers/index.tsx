import { createFileRoute } from "@tanstack/react-router";
import JobPostingForm from "../../../features/job-seeker/components/JobPostingForm";
import Header from "../../../features/landingPages/components/Header";

export const Route = createFileRoute("/_app/jobSeekers/")({
  component: RouteComponent,
});

function RouteComponent() {
  return (
    <>
      <Header />
      <JobPostingForm />
    </>
  );
}
