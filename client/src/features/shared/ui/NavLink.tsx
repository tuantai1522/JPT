// src/components/NavLink.tsx
import { twMerge } from "tailwind-merge";

type NavLinkProps = {
  children: string;
  href?: string;
  className?: string;
} & React.AnchorHTMLAttributes<HTMLAnchorElement>;

export default function NavLink({
  children,
  href = "#",
  className,
  ...props
}: NavLinkProps) {
  const base =
    "text-gray-600 hover:text-gray-900 transition-colors font-medium";

  return (
    <a href={href} {...props} className={twMerge(base, className)}>
      {children}
    </a>
  );
}
