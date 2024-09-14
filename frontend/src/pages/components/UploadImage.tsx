import axios from 'axios';

interface props {
    setAvatar: React.Dispatch<React.SetStateAction<string>>;
}

const UploadImage = (props: props) => {
    const handleImageChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const file = event.target.files?.[0]; // Get the selected file
        if (file) {
          const formData = new FormData();
          formData.append('file', file); // Append the file to the FormData
          uploadImage(formData);
        }
      };
    
      const uploadImage = (formData: FormData) => {
          axios
            .post('https://api.escuelajs.co/api/v1/files/upload', formData, {
              headers: {
                'Content-Type': 'multipart/form-data',
              },
            })
            .then((res) => {
              props.setAvatar(res.data.location);
            })
            .catch((err) => console.error(err));
      };

      return (
        <input type="file" id="screenshot" name="screenshot" accept="image/png, image/gif, image/jpeg" onChange={handleImageChange}/>
      )
}

export default UploadImage;