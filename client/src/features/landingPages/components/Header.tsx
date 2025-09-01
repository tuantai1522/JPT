import { Briefcase } from "lucide-react";
import Button from "../../shared/ui/Button";
import NavLink from "../../shared/ui/NavLink";
import { useGetCurrentUser } from "../../shared/hooks/queries/useGetCurrentUser";
import { useNavigate } from "@tanstack/react-router";

const Header = () => {
  const { data: user, isLoading } = useGetCurrentUser();
  const navigate = useNavigate();

  if (isLoading) {
    return <div>...</div>;
  }

  return (
    <header>
      <div className="container mx-auto px-4">
        <div className="flex items-center justify-between h-16">
          <div className="flex items-center space-x-3">
            <div className="w-8 h-8 bg-gradient-to-r from-blue-600 to-purple-600 rounded-lg flex items-center justify-center">
              <Briefcase className="w-5 h-5 text-white" />
            </div>
            <span className="text-xl font-bold text-gray-900">JobPortal</span>
          </div>

          <nav className="hidden md:flex items-center space-x-8">
            <NavLink>Find jobs</NavLink>
            <NavLink>For employers</NavLink>
          </nav>

          <div className="flex items-center space-x-3">
            {user ? (
              <div className="flex items-center space-x-3">
                <span className="text-gray-700">Welcome {user.firstName}</span>
                <Button>Dashboard</Button>
              </div>
            ) : (
              <div className="flex">
                <NavLink
                  onClick={() => navigate({ to: "/login" })}
                  className="px-4 py-2 rounded-lg hover:bg-gray-50"
                >
                  Log in
                </NavLink>
                <Button onClick={() => navigate({ to: "/register" })}>
                  Sign up
                </Button>
              </div>
            )}
          </div>
        </div>
      </div>
    </header>
  );
};

export default Header;
