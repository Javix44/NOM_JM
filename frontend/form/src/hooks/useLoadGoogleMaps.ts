import { useEffect, useState } from 'react';

export const useLoadGoogleMaps = () => {
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    const apiKey = process.env.REACT_APP_GOOGLE_API_KEY;
    if (!apiKey) return;

    const scriptId = 'google-maps-script';
    if (document.getElementById(scriptId)) {
      setLoaded(true);
      return;
    }

    const script = document.createElement('script');
    script.id = scriptId;
    script.src = `https://maps.googleapis.com/maps/api/js?key=${apiKey}&libraries=places`;
    script.async = true;
    script.defer = true;
    script.onload = () => setLoaded(true);

    document.body.appendChild(script);
  }, []);

  return loaded;
};