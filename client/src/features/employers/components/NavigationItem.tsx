import { Link } from "@tanstack/react-router";
import { cn } from "../../../lib/utils/cn";

type NavigationItemProps = {
  to: string;
  icon: React.ReactNode;
} & React.AnchorHTMLAttributes<HTMLAnchorElement>;

export default function NavigationItem({
  children,
  to,
  icon,
  ...props
}: NavigationItemProps) {
  const base =
    "w-full flex items-center px-3 py-2.5 text-sm font-medium rounded-lg transition-all duration-200 gap-3";

  return (
    <Link
      to={to}
      {...props}
      className={cn(base, "text-gray-600 hover:bg-gray-50 hover:text-red-500")}
      activeProps={{
        className: cn("bg-blue-50 text-blue-700 shadow-sm shadow-blue-50"),
      }}
      activeOptions={{ exact: true }}
    >
      {({ isActive }) => (
        <>
          <div
            className={cn(
              "w-full flex items-center px-3 py-2.5 text-sm font-medium rounded-lg transition-all duration-200 gap-3",
              "hover:bg-gray-50",
              isActive ? "bg-blue-100 text-blue-700 shadow-sm" : "text-gray-600"
            )}
          >
            <span
              className={cn(
                "h-5 w-5 flex-shrink-0",
                isActive ? "text-blue-700" : "text-gray-400"
              )}
            >
              {icon}
            </span>

            <span
              className={cn(
                "ml-2 truncate",
                isActive ? "text-blue-700 font-semibold" : "text-gray-600"
              )}
            >
              {children}
            </span>
          </div>
        </>
      )}
    </Link>
  );
}
