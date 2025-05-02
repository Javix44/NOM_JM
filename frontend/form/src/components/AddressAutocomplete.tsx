// // src/components/AddressAutocomplete.tsx

// import React, { useEffect, useRef } from 'react';
// import { Input } from 'antd';
// import type { InputRef } from 'antd';

// interface Props {
//   value: string;
//   onChange: (value: string) => void;
// }

// declare global {
//   interface Window {
//     google: typeof google;
//   }
// }

// const AddressAutocomplete: React.FC<Props> = ({ value, onChange }) => {
//   const inputRef = useRef<InputRef>(null);

//   useEffect(() => {
//     const input = inputRef.current?.input;

//     if (!input || typeof window.google === 'undefined') return;

//     const autocompleteInstance = new window.google.maps.places.Autocomplete(input, {
//       types: ['geocode'],
//     });

//     autocompleteInstance.addListener('place_changed', () => {
//       const place = autocompleteInstance.getPlace();
//       if (place.formatted_address) {
//         onChange(place.formatted_address);
//       }
//     });

//     return () => {
//       window.google.maps.event.clearInstanceListeners(autocompleteInstance);
//     };
//   }, []);

//   return (
//     <Input
//       ref={inputRef}
//       value={value}
//       onChange={(e) => onChange(e.target.value)}
//       placeholder="Start typing address..."
//     />
//   );
// };

// export default AddressAutocomplete;
