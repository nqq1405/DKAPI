# Secure Nginx with Let's Encrypt (On Linux)
## Introduction
    
- Let’s Encrypt là Tổ chức phát hành chứng chỉ (CA) cung cấp cách dễ dàng để lấy và cài đặt chứng chỉ [TLS / SSL miễn phí](https://www.digitalocean.com/community/tutorials/openssl-essentials-working-with-ssl-certificates-private-keys-and-csrs), do đó cho phép HTTPS được mã hóa trên máy chủ web. Nó đơn giản hóa quy trình bằng cách cung cấp một ứng dụng khách phần mềm, Certbot, cố gắng tự động hóa hầu hết (nếu không phải tất cả) các bước cần thiết. Hiện tại, toàn bộ quá trình lấy và cài đặt chứng chỉ hoàn toàn tự động trên cả Apache và Nginx.

## On CentOS (Use Certbot)
- [how-to-secure-nginx-with-let-s-encrypt-on-centos](https://www.digitalocean.com/community/tutorials/how-to-secure-nginx-with-let-s-encrypt-on-centos-7)
 
## On Ubuntu (Use Certbot) 
- [how-to-secure-nginx-with-let-s-encrypt-on-ubuntu-20-04](https://www.digitalocean.com/community/tutorials/how-to-secure-nginx-with-let-s-encrypt-on-ubuntu-20-04)

> Trong hướng dẫn này, bạn sẽ sử dụng Certbot để lấy chứng chỉ SSL miễn phí cho Nginx trên Ubuntu 20.04 và thiết lập tự động gia hạn chứng chỉ của bạn.

   * > Hướng dẫn này sẽ sử dụng tệp cấu hình máy chủ Nginx riêng biệt thay vì tệp mặc định. [Chúng tôi khuyên bạn nên tạo các tệp khối máy chủ Nginx](https://www.digitalocean.com/community/tutorials/how-to-install-nginx-on-ubuntu-20-04#step-5-%E2%80%93-setting-up-server-blocks-(recommended)) mới cho từng miền vì nó giúp tránh các lỗi thường gặp và duy trì các tệp mặc định dưới dạng cấu hình dự  phòng.

* ## ***Điều kiện***:
    -  Một máy chủ Ubuntu 20.04 được thiết lập bằng cách thực hiện theo thiết lập máy chủ ban đầu này cho [hướng dẫn Ubuntu 20.04](https://www.digitalocean.com/community/tutorials/initial-server-setup-with-ubuntu-20-04), bao gồm một người dùng không phải root đã kích hoạt sudo và tường lửa.

    - Tên miền đã đăng ký. Hướng dẫn này sẽ sử dụng example.com xuyên suốt. Bạn có thể mua một tên miền từ Namecheap, nhận một tên miền miễn phí với Freenom hoặc sử dụng công ty đăng ký tên miền mà bạn chọn.

    - Cả hai bản ghi DNS sau được thiết lập cho máy chủ của bạn. Nếu bạn đang sử dụng DigitalOcean, vui lòng xem tài liệu DNS của chúng tôi để biết chi tiết về cách thêm chúng.
        
        - Bản ghi A với example.com trỏ đến địa chỉ IP công cộng của máy chủ của bạn.
        - Bản ghi A với www.example.com trỏ đến địa chỉ IP công cộng của máy chủ của bạn.

1. Install Certbot and it’s Nginx plugin with apt:

- The first step to using Let’s Encrypt to obtain an SSL certificate is to install the Certbot software on your server.

    `$ sudo apt install certbot python3-certbot-nginx`

    The first step to using Let’s Encrypt to obtain an SSL certificate is to install the Certbot software on your server.

2. Confirming Nginx’s Configuration

- Certbot needs to be able to find the correct server block in your Nginx configuration for it to be able to automatically configure SSL. Specifically, it does this by looking for a server_name directive that matches the domain you request a certificate for.

- If you followed the server block set up step in the Nginx installation tutorial, you should have a server block for your domain at /etc/nginx/sites-available/example.com with the server_name directive already set appropriately.

- To check, open the configuration file for your domain using nano or your favorite text editor:

```
$ sudo nano /etc/nginx/sites-available/example.com
```

Find the existing `server_name` line. It should look like this: