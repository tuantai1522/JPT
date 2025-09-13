import { EMPLOYER_DASHBOARD } from "../../../lib/utils/data";
import StatCard from "./StatCard";

// Todo: To add API get dashboard statistics
export default function StatDashboard() {
  return (
    <>
      <div className="grid grid-cols-1 sm:grid-cols-3 gap-6">
        {EMPLOYER_DASHBOARD.map((item) => (
          <StatCard
            key={item.key}
            title={item.title}
            value={"123"}
            icon={item.icon}
            color={item.color}
          />
        ))}
      </div>
    </>
  );
}
