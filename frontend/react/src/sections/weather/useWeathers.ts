import { useState, useEffect } from 'react';
import { Subscription } from 'rxjs';
import { get } from '../../api/apiService';
import { ApiUrl } from '../../constant/url';

export interface Weather {
	date: string;
	temperatureC: number;
	temperatureF: number;
	summary: number;
}

export function useWeathers(): any {
	const [weathers, setWeathers] = useState<Weather[]>([]);
	const [loading, setLoading] = useState<boolean>(true);
	const [error, setError] = useState<string | null>(null);
  
	useEffect(() => {
		console.log('useEffect triggered');

		if (!get || !ApiUrl.weatherApi) {
		  console.error('get function or ApiUrl.weatherApi is not defined');
		  setError('Internal error');
		  setLoading(false);
		  return;
		}
	  const subscription: Subscription = get(ApiUrl.weatherApi).subscribe({
		next: (result) => {
		  setWeathers(result);
		  setLoading(false);
		},
		error: (err) => {
		  setError(err.message);
		  setLoading(false);
		},
	  });
  
	  // Cleanup subscription on unmount
	  return () => {
		subscription.unsubscribe();
	  };
	}, []);
  
	return { weathers, loading, error };
}
