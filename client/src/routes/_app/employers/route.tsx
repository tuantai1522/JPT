import { createFileRoute, Outlet } from "@tanstack/react-router";
import { ProtectedRoutes } from "../../../features/auth/components/ProtectedRoutes";
import NavigationItem from "../../../features/employers/components/NavigationItem";
import { NAVIGATION_MENU } from "../../../lib/utils/data";
import LogOutButton from "../../../features/shared/components/LogoutButton";
import ProfileDropDown from "../../../features/shared/components/ProfileDropDown";

export const Route = createFileRoute("/_app/employers")({
  component: EmployersLayout,
});

function EmployersLayout() {
  return (
    <ProtectedRoutes allowedRoles={["Employer"]}>
      <div className="flex h-screen">
        {/* Sidebar */}
        <aside className="w-64 bg-white p-4 space-y-2 flex flex-col justify-between">
          <div>
            {NAVIGATION_MENU.map((item) => (
              <NavigationItem key={item.id} to={item.id} icon={item.icon}>
                {item.name}
              </NavigationItem>
            ))}
          </div>
          <div>
            <LogOutButton />
          </div>
        </aside>

        {/* Content */}
        <div className="flex flex-col flex-1">
          <header className="bg-white/80 backdrop-blur-sm border-gray-200 h-16 flex items-center justify-between px-6 sticky top-0 z-30">
            <div>
              <h1 className="text-base font-semibold text-gray-900">
                Welcome back!
              </h1>
              <p className="text-sm text-gray-500 hidden sm:block">
                Here's what happening with your jobs today
              </p>
            </div>

            <div className="flex items-center space-x-3">
              <ProfileDropDown />
            </div>
          </header>
          <main className="flex-1 p-6 overflow-y-auto">
            <Outlet />
          </main>
        </div>
      </div>
    </ProtectedRoutes>
  );
}
