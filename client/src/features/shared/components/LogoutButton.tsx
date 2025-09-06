import { LogOut } from "lucide-react";
import { useLogOut } from "../hooks/queries/useLogout";
import { useQueryClient } from "@tanstack/react-query";
import { useNavigate } from "@tanstack/react-router";
import { useAuth } from "../../auth/contexts/useAuth";

const LogOutButton = () => {
  const logOutMutation = useLogOut();
  const navigate = useNavigate();
  const { setToken } = useAuth();

  const queryClient = useQueryClient();

  const handleClick = () => {
    logOutMutation.mutate(undefined, {
      onSuccess: async () => {
        // invalidate current user query
        queryClient.removeQueries({ queryKey: ["me"] });

        setToken(null);

        navigate({ to: "/", replace: true });
      },
      onError: (err) => {
        console.error("Logout failed", err);
      },
    });
  };

  return (
    <button
      onClick={handleClick}
      type="button"
      className={`
        w-full flex items-center gap-3 px-3 py-2.5
        text-sm font-medium rounded-lg
        transition-all duration-200
        text-gray-600
        hover:bg-gray-50 hover:text-gray-900
        focus:outline-none focus:ring-2 focus:ring-blue-500
        cursor-pointer
      `}
    >
      <LogOut className="h-5 w-5 flex-shrink-0 text-gray-400 group-hover:text-gray-600" />
      <span className="truncate">Log out</span>
    </button>
  );
};

export default LogOutButton;
