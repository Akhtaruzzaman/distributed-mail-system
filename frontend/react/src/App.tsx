import { WeatherCard } from "./sections/weather/WeatherCard";
import { Weather, useWeathers } from "./sections/weather/useWeathers";

export function App() {
	const data = useWeathers();

	if (data.weathers.length > 0) {
		return (
			<div className="App">
				<h3>⚡⚛️ Vite React Weather</h3>
				<h2>Weather forecast</h2>
				<table>
					<tr>
						<th>Date</th>
						<th>Temparature</th>
						<th>Summary</th>
					</tr>
					{data.weathers.map((user: Weather) => (
						<WeatherCard key={user.summary} user={user} />
					))}
				</table>
			</div>
		);
	} else {
		return <div></div>;
	}
}
