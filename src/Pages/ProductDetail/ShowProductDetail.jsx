import { Center, Box, Button, Flex, Grid, GridItem, Heading, Image, Input, ListItem, Text, UnorderedList, useToast } from '@chakra-ui/react';
import axios from 'axios';
import React, { useState, useEffect } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { useDispatch, useSelector } from 'react-redux';
// import { getProductDetail } from '../../Redux/ProductDetail/ProductDetail.action';
import { RotatingLines } from "react-loader-spinner";

const ShowProduct = () => {
    const { code } = useParams();
    const [singleData, setSingleData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
  
    const fetchData = async (productCode) => {
      try {
        const token = "c825f522ca3f4a5f935f2d2ac3e05e25";
        const axiosInstance = axios.create({
          headers: {
            'Key-Domain': `${token}`
          }
        });
        const response = await axiosInstance.get(`https://localhost:44333/api/ProductDetail/GetByCode?code=${productCode}`);
        setSingleData(response.data.result);
        setLoading(false);
      } catch (error) {
        console.error('Error fetching data:', error);
        setError(error);
        setLoading(false);
      }
    };
  
    useEffect(() => {
      fetchData(code);
    }, [code]);

    // const handlePost = (data) => {
    //     if (!data) return; // Kiểm tra xem có dữ liệu để gửi hay không

    //     let newData = { ...data };
    //     delete newData.id; // Bỏ qua trường "id" nếu bạn không muốn gửi nó

    //     axios.post(
    //         `https://rus-digital-televisions.onrender.com/cart`,
    //         newData,
    //         {
    //             headers: { "Content-Type": "application/json" },
    //         }
    //     )
    //         .then((response) => {
    //             toast({
    //                 title: "Added Item Successfully",
    //                 status: "success",
    //                 duration: 3000,
    //                 isClosable: true,
    //                 position: "bottom",
    //             });
    //             setTimeout(() => {
    //                 navigate("/cart");
    //             }, 1000);
    //         })
    //         .catch((error) => {
    //             console.log("Error in the postSingleData function:", error.response.data);
    //         });
    // };

    // const handleWish = (data) => {
    //     if (!data) return; // Kiểm tra xem có dữ liệu để gửi hay không

    //     let newData = { ...data };
    //     delete newData.id; // Bỏ qua trường "id" nếu bạn không muốn gửi nó

    //     axios.post(
    //         `https://rus-digital-televisions.onrender.com/whishlist`,
    //         newData,
    //         {
    //             headers: { "Content-Type": "application/json" },
    //         }
    //     )
    //         .then(() => {
    //             navigate("/whishlist");
    //         })
    //         .catch((error) => {
    //             console.log("Error in the postSingleDataWish function:", error.response.data);
    //         });
    // };

    if (error) {
        return (
            <Heading
                size="3xl"
                textAlign="center"
                color="red.500"
                marginTop={10}
                marginBottom="200px"
            >
                Something went wrong...
            </Heading>
        );
    }

    return (
   <div>
   {Array.isArray(singleData) &&
     singleData.map((item, index) => (
                <Box marginTop={12}>
                    <Grid
                        h={["1300px", "1100px", "600px"]}
                        templateRows={[
                            "repeat(8, 1fr)",
                            "repeat(8, 1fr)",
                            "repeat(8, 1fr)",
                        ]}
                        templateColumns={[
                            "repeat(4, 1fr)",
                            "repeat(6, 1fr)",
                            "repeat(10, 1fr)",
                        ]}
                    >
                        <GridItem
                            rowSpan={[1, 2, 4]}
                            colSpan={[4, 6, 4]}
                            display="flex"
                            justifyContent="center"
                            alignItems="center"
                            style={{
                                boxShadow:
                                    "rgba(0, 0, 0, 0.02) 0px 1px 3px 0px, rgba(27, 31, 35, 0.15) 0px 0px 0px 1px",
                            }}
                        >
                            <Image
                                  src={item?.code || 'https://cdn2.cellphones.com.vn/insecure/rs:fill:358:358/q:80/plain/https://cellphones.com.vn/media/catalog/product/5/h/5h53.png'} // Sử dụng toán tử ?. để an toàn truy cập
                                  alt={item?.name || ''}
                                _hover={{ cursor: "crosshair" }}
                            />
                        </GridItem>
                        <GridItem
                            colSpan={[4, 6, 6]}
                            rowSpan={1}
                            p={5}
                            style={{
                                boxShadow:
                                    "rgba(0, 0, 0, 0.02) 0px 1px 3px 0px, rgba(27, 31, 35, 0.15) 0px 0px 0px 1px",
                            }}
                        >
                            <Text color="gray.500" marginBottom={5}>
                                Article ID: {item.code}
                            </Text>
                            <Heading size="md" marginBottom={5}>
                                {item.code}
                            </Heading>
                        </GridItem>
                        <GridItem
                            colSpan={[4, 3, 3]}
                            rowSpan={7}
                            style={{
                                boxShadow:
                                    "rgba(0, 0, 0, 0.02) 0px 1px 3px 0px, rgba(27, 31, 35, 0.15) 0px 0px 0px 1px",
                            }}
                            p={5}
                        >
                            <Heading size="sm" marginBottom={3}>
                                Gain {item.code}more with offers (4)
                            </Heading>
                            <UnorderedList color="gray.600" fontSize="sm" marginBottom={4}>
                                <ListItem>
                                    Wall mount bracket is on chargeable basis.{" "}
                                    <span style={{ color: "#2871c4" }}>Read T&C</span>
                                </ListItem>
                                <ListItem>
                                    Buy RCP warranty and save up to 55%.{" "}
                                    <span style={{ color: "#2871c4" }}>Read T&C</span>
                                </ListItem>
                                <ListItem>
                                    Shop for Rs.20,000 & above and get instant discount Up To
                                    Rs.5000,Use coupon codes "YES1000" for above 20,000 ,"YES2500"
                                    for above 50,000, "YES5000" for above 1,00,000..{" "}
                                    <span style={{ color: "#2871c4" }}>Read T&C</span>
                                </ListItem>
                                <ListItem>
                                    Get Cashback upto Rs. 1,000 on Mobikwik Wallet.{" "}
                                    <span style={{ color: "#2871c4" }}>Read T&C</span>
                                </ListItem>
                            </UnorderedList>
                            <Heading size="sm" marginBottom={3}>
                                Save more with EMI/Cashback (1){" "}
                                <span
                                    style={{
                                        fontWeight: "bold",
                                        fontSize: "12px",
                                        color: "#2871c4",
                                    }}
                                >
                                    Read T&C
                                </span>
                            </Heading>
                            <UnorderedList color="gray.600" fontSize="sm" marginBottom={4}>
                                <ListItem>
                                    EMIs (Credit Cards) from ₹792.16/month |{" "}
                                    <span style={{ color: "#2871c4" }}>
                                        View all Standard Credit Cards EMI options
                                    </span>
                                </ListItem>
                            </UnorderedList>
                            <Heading size="sm" marginBottom={3}>
                                Warranty
                            </Heading>
                            <UnorderedList color="gray.600" fontSize="sm" marginBottom={5}>
                                <ListItem>
                                    <span style={{ fontWeight: "bold" }}>Warranty:</span> 1 Year
                                    manufacturer warranty
                                </ListItem>
                            </UnorderedList>
                            <Heading size="sm" marginBottom={3}>
                                Additional Services & Warranties (3){" "}
                                <span
                                    style={{
                                        fontWeight: "bold",
                                        fontSize: "14px",
                                        color: "#2871c4",
                                    }}
                                >
                                    View All
                                </span>
                            </Heading>
                        </GridItem>
                        <GridItem
                            colSpan={[4, 3, 3]}
                            rowSpan={7}
                            p={5}
                            style={{
                                boxShadow:
                                    "rgba(0, 0, 0, 0.02) 0px 1px 3px 0px, rgba(27, 31, 35, 0.15) 0px 0px 0px 1px",
                            }}
                        >
                            <Heading size="lg" marginBottom={5} color="blue.700">
                                ₹{item.price}
                            </Heading>
                            <Text fontSize="lg" marginBottom={3}>
                                MRP:{" "}
                                <span style={{ textDecoration: "line-through" }}>
                                    {item.mrp}
                                </span>{" "}
                                <span style={{ fontSize: "12px" }}>
                                    (Inclusive of all taxes)
                                </span>
                            </Text>

                            <Text
                                fontSize="sm"
                                color="green.600"
                                style={{ fontWeight: "bold" }}
                                marginBottom={3}
                            >
                                You Save: {item.discount}
                            </Text>

                            <Text
                                fontSize="sm"
                                style={{ fontWeight: "bold" }}
                                marginBottom={3}
                            >
                                EMIs (Credit Cards) from ₹792.16/month |{" "}
                                <span style={{ color: "#2871c4" }}>View Plans</span>
                            </Text>

                            <Text
                                fontSize="lg"
                                style={{ fontWeight: "bold" }}
                                marginBottom={3}
                            >
                                FREE Shipping!
                            </Text>

                            <Input
                                w="70%"
                                borderRadius="none"
                                placeholder="Enter / Detect PIN Code"
                                p={2}
                                marginBottom={3}
                            ></Input>

                            <Flex w="full" justifyContent="space-between">
                                <Button
                                    w="49%"
                                    color="white"
                                    bg="red"
                                    borderRadius="sm"
                                    fontSize="lg"
                                    p={6}
                                    _hover={{ bg: "blue.800" }}
                                   // onClick={() => handlePost(item)}
                                >
                                    ADD TO CART
                                </Button>
                                <Button
                                    w="49%"
                                    color="white"
                                    bg="orangered"
                                    borderRadius="sm"
                                    fontSize="lg"
                                    p={6}
                                    _hover={{ backgroundColor: "orangered" }}
                                   // onClick={() => handleWish(item)}
                                >
                                    Add to Wishlist
                                </Button>
                            </Flex>
                        </GridItem>
                        {/* <button onClick={() => handleDelete(item.id)}>delete</button> */}

                    </Grid>
                </Box>
     ))}
 </div>
 
    );
};

export { ShowProduct };