# Đăng kiểm API (v1) (đang cập nhật lại tài liệu.....)
## 1. **City**
## - *Dữ liệu Thành Phố*
---
| Title       | URL       | Method | URL Params | Data Params | StatusCode Response | StatusCode Error Response |
| ----------- | ---       | ------ | ---------- | ----------- | ---------------- | -------------- | 
| Show Citys  | /api/City | `GET` | None | None | 200 |  400|

* Sample Response Content: Object
```JSON
[
   {
      "cityId": 1,
      "name": "Hà Nội"
   },
   ....
]
```
# 2. **Price**
## 2.1 *Dữ liệu giá của tất cả các loại xe*
---
| Title       | URL       | Method | URL Params | Data Params | StatusCode Response | StatusCode Error Response |
| ----------- | ---       | ------ | ---------- | ----------- | ---------------- | -------------- | 
| Show Vehicle Prices  | /api/Price | `GET` | None | None | `200` |  `500`|
* Sample Response Content: Object
```JSON
{
  "pkdlist": [
    {
      "_id": "6194c6514c5c9b4d8aa33849",
      "vehicleId": 1,
      "priceKD": 240000,
      "priceCert": 100000,
      "timeUpdate": "11-29-2021 11:39:13"
    },
   ....
   ....
  ],
  "pdblist": [
    {
      "_id": "6194c65e4c5c9b4d8aa33856",
      "vehicleId": 12,
      "priceDB": 130000,
      "timeUpdate": null
    },
   ....
   ....
]
```

## 2.1 *Dữ liệu giá cua xe theo vehicleId*
---
| Title                           | URL        | Method | URL Params | Data Params | StatusCode Response | StatusCode Error Response |
| -------------------------------   | --------------  | ------ | ---------- | ----------- | ---------------- | -------------- | 
| Show Vehicle Price by vehicleid  | /api/Price/{vehicleId} | `GET` | `{vehicleId}` | None | 200 |  `400` \| `404`|

* Data Params Required: vehicleId = [interger]

* Sample Response Content: Object 
   
   * Code: 200 OK
```JSON
{
   "vehicleId": 12,
   "typeVehicle": "LDB",
   "nameVehicle": "Xe chở người dưới 10 chỗ đăng ký tên cá nhân"
},
```
* **Sample Error Response:**

   URL:  **{host}/api/Price/sds**
   * Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value 'sds' is not valid."
         ]
      }
   }
   ```

   * Code: 404 NotFound
   * Content: 
   ```JSON
   {
   "message": "Not Found"
   }
   ```

## 2.1 **Cập nhất Giá của 2 loại xe đường bộ và kiểm định**
---
| Title                             | URL                        | Method | URL Params    | Data Params | StatusCode Response | StatusCode Error Response|
| --------------------------------- | -------------------------- | ------ | ------------- | ----------- | ------------------- | ------------------------ |
| PKD                               | /api/Price/PKD/{vehicleId} | `PUT`  | `{vehicleId}` | `Object`    | 201                 |  `400` \| `404`|
| PDB                               | /api/Price/PDB/{vehicleId} | `PUT`  | `{vehicleId}` | `Object`    | 201                 |  `400` \| `404`|

* Data Params Required: vehicleId = [interger]
* Data Body Required: 
* With /api/Price/PKD/{vehicleId}
```JSON
"Object" = {
  "priceKD": 100000,
  "priceCert": 50000
}
```
* With /api/Price/PDB/{vehicleId}
```JSON
"Object" = {
  "priceDB": 50000
}
```


* Sample Response Content: Object 
   
   * Code: 201 NoContent

* **Sample Error Response:**
   
   * Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value 'sds' is not valid."
         ]
      }
   }
   ```

   * Code: 404 NotFound
   * Content: 
   ```JSON
   {
   "message": "Not Found"
   }
   ```
## 3. **Station**
## 3.1 *Dữ liệu trạm đăng kiểm*
---
| Title       | URL       | Method | URL Params | Data Params | StatusCode Response | StatusCode Error Response |
| ----------- | ---       | ------ | ---------- | ----------- | ---------------- | -------------- | 
| Show Stations  | /api/station | `GET` | None | None | `200` | `500` |

* Sample Response Content: Object
```JSON
[
  {
    "cityId": 1,
    "stationId": 1,
    "stationName": "Quận Bắc Từ Liêm -Km số 3, đường Phạm Văn Đồng, phường Cổ Nhuế 1, Tp Hà Nội/Trung Tâm đăng kiểm xe cơ giới 2927D "
  },
   ....
]
```
## 3.2 *thông tin trạm đăng kiểm theo thành phố*
---
| Title          | URL                     | Method | URL Params   | Data Params  | StatusCode Response | StatusCode Error Response |
| -------------- | ----------------------- | ------ | ------------ | ------------ | ------------------- | ------------------------- | 
| Show Stations  | /api/station/{byCityId} | `GET`  | `{byCityId}` | None         | `200`               | `400` \| `404`            |

* Data Params Required: byCityId = [interger]

* Sample Response Content: Object

URL: /api/Station/1
```JSON
[
  {
    "cityId": 1,
    "stationId": 1,
    "stationName": "Quận Bắc Từ Liêm -Km số 3, đường Phạm Văn Đồng, phường Cổ Nhuế 1, Tp Hà Nội/Trung Tâm đăng kiểm xe cơ giới 2927D "
  },
   ....
]
```

* **Sample Error Response:**
* Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value '' is not valid."
         ]
      }
   }
   ```
* Code: 404 NotFound
   * Content: 
   ```JSON
   {
   "message": "Not Found"
   }
   ```

# 4. Time
| Title        | URL                  | Method | URL Params   | Data Params | StatusCode Response | StatusCode Error Response |
| ------------ | ---------------------| ------ | ------------ | ----------- | ---------------- | -------------- | 
| Get TimeSlot | /api/time?{datetime} | `GET`  | `{datetime}` | None | `200` | `400` \| `404` |

* Data Params Required: datetime = [DateTime]

* Sample Response Content: Object

URL: /api/Time?datetime=12-11-2021
```JSON
[
  {
    "time": "08:00",
    "slot": 5
  },
   ....
]
```

* **Sample Error Response:**
* Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value '' is not valid."
         ]
      }
   }
   ```
* Code: 404 NotFound
   * Content: 
   ```JSON
   {
   "message": "Not Found"
   }
   ```
# 5. User
## 5.1 chi phí tạm tính của người dùng đăng kiểm
---
| Title          | URL            | Method | URL Params  | Data Params | StatusCode Response | StatusCode Error Response |
| -------------- | ---------------| ------ | ----------- | ----------- | ------------------- | ------------------------- |
| Show Tempcosts | /api/tempcosts | `POST` | None        | `Object`    | `200`               | `400` \| `404`            |

* Request body
```json
"Object" = {
  "vehiclePdbId": 12,
  "vehiclePkdId": 1,
  "year": 2020,
  "uses": true,
  "service": true
}
```

* **Success Response:**

    Code: 200
    Content:
    ```json
   "Object" = {
   "costPkd": "240.000 VNĐ", 
   "costCert": "100.000 VNĐ", 
   "costPdb": "780.000 VNĐ", 
   "costService": "1.000.000 VNĐ", 
   "costTotalTemp": "2.120.000 VNĐ" 
   }
   ```
* **Sample Error Response:**
* Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value '' is not valid."
         ]
      }
   }
   ```
* Code: 404 NotFound
   * Content: 
   ```JSON
   {
   "message": "Not Found"
   }
   ```
## 5.2 Tạo người dùng đặt lịch
---
| Title          | URL                | Method | URL Params  | Data Params | StatusCode Response | StatusCode Error Response |
| -------------- | ------------------ | ------ | ----------- | ----------- | ------------------- | ------------------------- |
| Create User    | /api/User/schedule | `POST` | None        | `Object`    | `200`               | `400` \| `404`            |

* **Request body**

   |Atribute            | DataType  | 
   | ------------------ | ----------| 
   | name               | string    | 
   | phoneNumber        | string    |
   | licensePlates      | string    |
   | cityId             | int       |
   | stationId          | int       |
   | vehiclePKDId       | int       |
   | vehiclePDBId       | int       |
   | carCompany         | string    | 
   | yearofManufacture  | int       |
   | isOwner            | bool      |
   | uses               | bool      |
   | useService         | bool      |
   | schedule           | DateTime  |
   | timeSlot           | string    |


* **Success Response:**

    Code: 200
    Content:
    ```json
   "Object" = {
  "name": "Nguyễn Văn A",
  "phoneNumber": "0912393883",
  "licensePlates": "30A-246.56",
  "cityId": 1,
  "stationId": 1,
  "vehiclePKDId": 1,
  "vehiclePDBId": 12,
  "carCompany": "Toyota",
  "yearofManufacture": 2020,
  "isOwner": true,
  "uses": true,
  "useService": true,
  "schedule": "2021-12-25T00:00:00.0000000",
  "timeSlot": "08:00"
}

* **Sample Error Response:**
* Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value '' is not valid."
         ]
      }
   }
   ```
* Code: 404 NotFound
   * Content: 
   ```JSON
   {
   "message": "Not Found"
   }
   ```

## 5.3 Lấy Thông tin User Đặt lịch theo ObjectId
---
| Title            | URL                  | Method | URL Params | Data Params | StatusCode Response | StatusCode Error Response |
| ---------------- | -------------------- | ------ | ---------- | ----------- | ------------------- | ------------------------- |
| Get User Sche    | /api/User/{objectId} | `GET`  |`{objectId}`| None        | `200`               | `404` \| `400`            |

* **Success Response:**

    Code: 200
    Content:
    ```json
   "Object" = {
  "name": "Nguyễn Văn A",
  "phoneNumber": "0912393883",
  "licensePlates": "30A-246.56",
  "cityId": 1,
  "stationId": 1,
  "vehiclePKDId": 1,
  "vehiclePDBId": 12,
  "carCompany": "Toyota",
  "yearofManufacture": 2020,
  "isOwner": true,
  "uses": true,
  "useService": true,
  "schedule": "2021-12-25T00:00:00.0000000",
  "timeSlot": "08:00"
}

* **Sample Error Response:**
* Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value '' is not valid."
         ]
      }
   }
   ```
* Code: 404 NotFound
   * Content: 
   ```JSON
   {
   "message": "Not Found"
   }
   ```


# 5.4 Cập nhật Phí Dịch Vụ

---
| Title               | URL                                    | Method | URL Params      | Data Params | StatusCode Response | StatusCode Error Response |
| ------------------- | ---------------------------------------| ------ | --------------- | ----------- | ------------------- | ------------------------- |
| Get Vehicle User    | /api/User/costs_service/{CostsService} | `GET`  | `{CostsService}`| None        | `200`               | `400`   \| `404`          |


* **Success Response:**

    Code: 204

* **Sample Error Response:**
* Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value '' is not valid."
         ]
      }
   }
   ```
* Code: 404 NotFound
   * Content: 
   ```JSON
   {
   "message": "Not Found"
   }

# 6. Vehicle
* Danh sách loại phương tiện theo loại
---
| Title               | URL                        | Method | URL Params      | Data Params | StatusCode Response | StatusCode Error Response |
| ------------------- | -------------------------- | ------ | --------------- | ----------- | ------------------- | ------------------------- |
| Get Vehicle User    | /api/vehicle?{vehicleType} | `GET`  | `{vehicleType}` | None        | `200`               | `400`  \| `404`           |

* **Success Response:**

    Code: 200

* **Sample Error Response:**
* Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value '' is not valid."
         ]
      }
   }
   ```

# 7. Year
* Danh sách năm (tính từ hiện tại đến trong khoảng 25 năm gần nhất)
---
| Title     | URL       | Method | URL Params | Data Params | StatusCode Response | StatusCode Error Response |
| --------- | --------- | ------ | ---------- | ----------- | ------------------- | ------------------------- |
| Get Year  | /api/Year | `GET`  | None       | None        | `200`               | `400`                     |


* **Success Response:**

    Code: 200
    ```json
    content: 
    Array[2021,
      2020,
      2019,
      2018,
      2017,
      2016,
      2015,
      2014,
      2013,
      2012,
      2011,
      2010,
      2009,
      2008,
      2007,
      2006,
      2005,
      2004,
      2003,
      2002,
      2001,
      2000,
      1999,
      1998,
      1997]
  ```

* **Sample Error Response:**
* Code: 400 Bad Request
   * Content: 
   ```JSON
   {
      {
      "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
      "title": "One or more validation errors occurred.",
      "status": 400,
      "traceId": "00-fe520c3e542477478af04e8179067f43-df0906cc2a2b3945-00",
      "errors": {
      "vehicleId": [
      "The value '' is not valid."
         ]
      }
   }
   ```