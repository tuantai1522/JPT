// src/components/NavLink.tsx
import { cn } from "../../../lib/utils/cn";

type NavLinkProps = {
  children: string;
  href?: string;
  className?: string;
} & React.AnchorHTMLAttributes<HTMLAnchorElement>;

export default function NavLink({
  children,
  href,
  className,
  ...props
}: NavLinkProps) {
  const base =
    "text-gray-600 hover:text-gray-900 transition-colors font-medium cursor-pointer";

  return (
    <a href={href} {...props} className={cn(base, className)}>
      {children}
    </a>
  );
}
