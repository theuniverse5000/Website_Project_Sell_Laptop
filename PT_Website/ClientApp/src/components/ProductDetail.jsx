import React, { useEffect, useState } from 'react';
import axios from 'axios';
import Card from '@mui/material/Card';
import CardActionArea from '@mui/material/CardActionArea';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Typography from '@mui/material/Typography';

const GetProductDetail = () => {
    const [data, setData] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        // Lấy token từ nơi bạn đã lưu trữ (localStorage, sessionStorage, Redux, ...)
        const token = "c825f522ca3f4a5f935f2d2ac3e05e25";

        // Tạo một instance Axios với cài đặt headers chứa token
        const axiosInstance = axios.create({
            headers: {
                'Key-Domain': `${token}`
            }
        });

        // Sử dụng Axios để gọi API thông qua axiosInstance
        const fetchData = async () => {
            try {
                const response = await axiosInstance.get('https://localhost:44333/api/ProductDetail/GetProductDetailsFSP');
                setData(response.data);
                setLoading(false);
            } catch (error) {
                console.error('Error fetching data:', error);
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    const cardStyles = {
        width: '270px',
        height: '370px',
        margin: '10px', // Khoảng cách giữa các thẻ
    };

    return (
        <div>
            {loading ? (
                <p>Loading...</p>
            ) : (
                <div style={{ display: 'flex', flexWrap: 'wrap' }}>
                    {Array.isArray(data.result) ? (
                        data.result.map((item, index) => (
                            <Card key={index} sx={cardStyles}>
                                <CardActionArea>
                                    <CardMedia
                                        component="img"
                                        style={{ width: '270px', height: '230px' }}
                                        image="data:image/jpeg;base64,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"
                                        alt="green iguana"
                                    />
                                    <CardContent>
                                        <Typography gutterBottom variant="h5" component="div">
                                           
                                        </Typography>
                                        <Typography variant="body2" color="text.secondary">
                                            {item.nameProduct}({item.code}, {item.tenCpu},{item.thongSoRam},{item.thongSoHardDrive})
                                            {item.description}
                                        </Typography>
                                    </CardContent>
                                </CardActionArea>
                            </Card>
                        ))
                    ) : (
                        <p>Data is not an array or has no 'result' property.</p>
                    )}
                </div>
            )}
        </div>
    );
};

export { GetProductDetail };
