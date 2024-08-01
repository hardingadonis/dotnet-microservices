#!/bin/bash

if [ "$#" -ne 1 ]; then
    echo "Usage: $0 [1|2|3|4]"
    echo "1: Get version from Catalog Service"
    echo "2: Get version from Basket Service"
    echo "3: Get version from Discount Service"
    echo "4: Get version from Ordering Service"
fi

case "$1" in
    1)
        CSPROJ_FILE="../src/Services/Catalog/Catalog.API/Catalog.API.csproj"
        ;;
    2)
        CSPROJ_FILE="../src/Services/Basket/Basket.API/Basket.API.csproj"
        ;;
    3)
        CSPROJ_FILE="../src/Services/Discount/Discount.API/Discount.API.csproj"
        ;;
    4)
        CSPROJ_FILE="../src/Services/Ordering/Ordering.API/Ordering.API.csproj"
        ;;
    *)
        echo "Invalid option"
        exit 1
        ;;
esac

if [ ! -f "$CSPROJ_FILE" ]; then
    echo "File not found: $CSPROJ_FILE"
    exit 1
fi

VERSION=$(grep -oP '<FileVersion>\K[^<]+' "$CSPROJ_FILE")

echo "$VERSION"