let scriptLoaded = false;

export const loadGoogleMapsScript = (): Promise<void> => {
  if (scriptLoaded) return Promise.resolve();

  return new Promise((resolve, reject) => {
    const existingScript = document.getElementById('google-maps-script') as HTMLScriptElement;

    if (existingScript) {
      existingScript.addEventListener('load', () => resolve());
      return;
    }

    const apiKey = import.meta.env.VITE_GOOGLE_MAPS_API_KEY || process.env.REACT_APP_GOOGLE_MAPS_API_KEY;

    if (!apiKey) {
      reject('Google Maps API key not found');
      return;
    }

    const script = document.createElement('script');
    script.id = 'google-maps-script';
    script.src = `https://maps.googleapis.com/maps/api/js?key=${apiKey}&libraries=places`;
    script.async = true;
    script.defer = true;
    script.onload = () => {
      scriptLoaded = true;
      resolve();
    };
    script.onerror = (err) => reject(err);
    document.head.appendChild(script);
  });
};
