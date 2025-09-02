import { Link } from "@tanstack/react-router";
import { cn } from "../../../lib/utils/cn";

type NavigationItemProps = {
  to: string;
  icon: React.ElementType;
} & React.AnchorHTMLAttributes<HTMLAnchorElement>;

export default function NavigationItem({
  children,
  to,
  icon,
  ...props
}: NavigationItemProps) {
  const base =
    "w-full flex items-center px-3 py-2.5 text-sm font-medium rounded-lg transition-all duration-200 gap-3";

  const Icon = icon;

  return (
    <Link
      to={to}
      {...props}
      className={cn(base, "text-gray-600 hover:bg-gray-50 hover:text-gray-900")}
      activeProps={{
        className: cn("bg-blue-50 text-blue-700 shadow-sm shadow-blue-50"),
      }}
      activeOptions={{ exact: true }}
    >
      {({ isActive }) => (
        <>
          <Icon
            className={`h-5 w-5 flex-shrink-0 ${
              isActive ? "text-blue-700 w-5 h-5" : "text-gray-400 w-5 h-5"
            }`}
          />

          <span
            className={`ml-2 truncate ${
              isActive ? "text-blue-700 font-semibold" : "text-gray-600"
            }`}
          >
            {children}
          </span>
        </>
      )}
    </Link>
  );
}
