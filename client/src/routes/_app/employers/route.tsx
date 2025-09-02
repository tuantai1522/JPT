import { createFileRoute, Outlet } from "@tanstack/react-router";
import { ProtectedRoutes } from "../../../features/auth/components/ProtectedRoutes";
import NavigationItem from "../../../features/employers/components/NavigationItem";
import { NAVIGATION_MENU } from "../../../lib/utils/data";

export const Route = createFileRoute("/_app/employers")({
  component: EmployersLayout,
});

function EmployersLayout() {
  return (
    <ProtectedRoutes allowedRoles={["Employer"]}>
      <div className="flex h-screen">
        {/* Sidebar */}
        <aside className="w-64 bg-white p-4 space-y-2">
          {NAVIGATION_MENU.map((item) => (
            <NavigationItem key={item.id} to={item.id} icon={item.icon}>
              {item.name}
            </NavigationItem>
          ))}
        </aside>

        {/* Content */}
        <main className="flex-1 p-6 overflow-y-auto">
          <Outlet />
        </main>
      </div>
    </ProtectedRoutes>
  );
}
