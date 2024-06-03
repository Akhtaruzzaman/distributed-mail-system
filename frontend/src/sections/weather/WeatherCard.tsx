import styles from "./UserCard.module.scss";
import { Weather } from "./useWeathers";

export function WeatherCard({ user }: { user: Weather }) {
	return (
		<tr>
			<td>{user.date}</td>
			<td>{user.temperatureC}</td>
			<td>{user.summary}</td>
		</tr>
	);
}
