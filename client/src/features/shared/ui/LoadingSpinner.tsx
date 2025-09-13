import { cn } from "../../../lib/utils/cn";
import type { HTMLAttributes } from "react";

type LoadingSpinnerProps = HTMLAttributes<HTMLDivElement>;

export default function LoadingSpinneer({
  className,
  ...props
}: LoadingSpinnerProps) {
  return (
    <>
      <div
        className={cn(
          "h-5 w-5 animate-spin rounded-full border-2 border-neutral-200 border-t-neutral-800 dark:border-neutral-800 dark:border-t-neutral-200"
        )}
        {...props}
      />
    </>
  );
}
