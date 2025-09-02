import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_app/employers/manage-jobs")({
  component: RouteComponent,
});

function RouteComponent() {
  return <div>Hello "/_app/employers/manage-job"!</div>;
}
