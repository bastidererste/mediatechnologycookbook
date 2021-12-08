//CMAKE_CXX_STANDARD 20
//g++ v9


#include <iostream>
#include <vector>
#include <cmath>

float map(float inValue, float minValue, float maxValue){

    return ( (inValue - minValue) / (maxValue - minValue));
}

int main() {

    std::vector<int> calPoint = {1000, 2000 , 3000, 4000, 5000, 6000 };
    std::vector<double> data = {11, 22 , 33, 44, 55, 66 };

    for (int i = 66; i < 7000; ++i) {

        auto val = std::lower_bound(calPoint.begin(), calPoint.end(), i);


        if(i >= *calPoint.begin()  && val != calPoint.end()){
            double a_calPoint = calPoint[std::distance(calPoint.begin(), val) - 1];
            double b_calPoint = calPoint[std::distance(calPoint.begin(), val)];

            double a_data = data[std::distance(calPoint.begin(), val) - 1];
            double b_data = data[std::distance(calPoint.begin(), val)];

            double m = map((double)i, a_calPoint,b_calPoint);

            double res = std::lerp(a_data, b_data, m);

            std::cout << res << std::endl;

        }

    }
    return 0;
}
