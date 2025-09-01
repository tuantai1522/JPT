import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/_auth/unauthorized")({
  component: UnauthorizedPage,
});

function UnauthorizedPage() {
  return <h1>You don't have permission to access this resource</h1>;
}
