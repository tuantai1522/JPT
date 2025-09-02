import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_app/employers/company-profile")({
  component: RouteComponent,
});

function RouteComponent() {
  return <div>Hello "/_app/employers/company-profile"!</div>;
}
