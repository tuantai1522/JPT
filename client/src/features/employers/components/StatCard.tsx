interface StatCardProps {
  title: string;
  value: string;
  icon: React.ReactNode;
  color: "blue" | "green" | "purple" | "orange";
}

export default function StatCard({ title, value, icon, color }: StatCardProps) {
  const colorClasses = {
    blue: "from-blue-500 to-blue-600",
    green: "from-emerald-500 to-emerald-600",
    purple: "from-violet-500 to-violet-600",
    orange: "from-orange-500 to-orange-600",
  };

  return (
    <>
      <div
        className={`bg-gradient-to-br ${colorClasses[color]} text-white bg-white rounded-xl border border-gray-100 shadow-sm hover:shadow-md transition-shadow duration-200`}
      >
        <div className="p-6">
          <div className="flex items-center justify-between">
            <div>
              <p className="text-white/80 text-sm font-medium">{title}</p>
              <p className="text-3xl font-bold mt-1">{value}</p>
            </div>
            <div className="bg-white/10 p-3 rounded-xl">{icon}</div>
          </div>
        </div>
      </div>
    </>
  );
}
