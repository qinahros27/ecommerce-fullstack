import React, { useState } from 'react';
import Rating from '@mui/material/Rating';

function StarRating() {
    const [rating, setRating] = useState<number | null>(0);

  return (
    <Rating
      name="simple-controlled"
      value={rating}
      precision={0.5} // Allows half-star ratings
      onChange={(event, newValue) => {
        setRating(newValue);
      }}
      size= 'small'
    />
  );
}

export default StarRating;