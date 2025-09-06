import { ChevronDown, ChevronRight } from "lucide-react";
import { useGetCurrentUser } from "../hooks/queries/useGetCurrentUser";
import { useNavigate } from "@tanstack/react-router";
import { UserRole } from "../constants/userRole";
import { useState } from "react";
import LogOutButton from "./LogoutButton";

const ProfileDropDown = () => {
  const { data: user, isLoading } = useGetCurrentUser();

  const navigate = useNavigate();

  if (isLoading) return <p>Loading...</p>;

  const [isOpen, setIsOpen] = useState<boolean>(false);

  return (
    <div className="relative">
      <button
        onClick={() => setIsOpen(!isOpen)}
        className="flex items-center space-x-3 p-2 rounded-xl hover:bg-gray-50 transition-colors duration-200 cursor-pointer"
      >
        {user?.avatarUrl ? (
          <img
            src={user.avatarUrl}
            alt="Avatar"
            className="h-9 w-9 object-cover rounded-xl"
          />
        ) : (
          <div className="h-8 w-8 bg-gradient-to-r from-blue-500 to-blue-600 rounded-xl flex items-center justify-center">
            <span className="text-white font-semibold text-sm">
              {user?.companyName?.charAt(0).toUpperCase()}
            </span>
          </div>
        )}
        <div className="hidden sm:block text-left">
          <p className="text-sm font-medium text-gray-900">{user?.firstName}</p>
          <p className="text-xs text-gray-500">{user?.role}</p>
        </div>
        {isOpen ? (
          <ChevronDown className="h-4 w-4 text-gray-400" />
        ) : (
          <ChevronRight className="h-4 w-4 text-gray-400" />
        )}
      </button>

      {isOpen && (
        <div className="absolute right-0 mt-2 w-56 bg-white rounded-xl shadow-lg border border-gray-100 py-2 z-50">
          <div className="px-4 py-3 border-b border-gray-100">
            <p className="text-sm font-medium text-gray-900">
              {user?.companyName}
            </p>
            <p className="text-xs text-gray-500">{user?.emaiL}</p>
            <p></p>
          </div>
          <a
            onClick={() =>
              navigate({
                to:
                  // user?.role === UserRole.JobSeeker
                  //   ? "/profile"
                  //   : "/company-profile",
                  "/employers",
              })
            }
            className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-300 transition-colors cursor-pointer"
          >
            View profile
          </a>
          <div className="border-t border-gray-100 mt-2 pt-2">
            <LogOutButton />
          </div>
        </div>
      )}
    </div>
  );
};

export default ProfileDropDown;
