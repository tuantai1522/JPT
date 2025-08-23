import { createFileRoute } from "@tanstack/react-router";
import RegisterForm from "../../features/auth/components/RegisterForm";

export const Route = createFileRoute("/auths/register")({
  component: RouteComponent,
});

function RouteComponent() {
  return <RegisterForm />;
}
