#include <iostream>
#include <vector>

using namespace std;

bool isleapyear(int year){
    if (year % 100 == 0)
        return year % 400 == 0;
    else
        return year % 4 == 0 && year % 100 != 0;
}

const int Jan = 31;
int inline Feb(int year){
    return isleapyear(year) ? 29 : 28;
}
const int Mar = 31;
const int Apr = 30;
const int May = 31;
const int Jun = 30;
const int Jul = 31;
const int Aug = 31;
const int Sep = 30;
const int Oct = 31;
const int Nov = 30;
const int Dec = 31;

int getfirstday(int year){
    if (year == 1900)
        return 0;
    else {
        return (getfirstday(year - 1) + (isleapyear(year - 1) ? 366 : 365)) % 7;
    }
}

void calculate_year(int year, vector<int>& days){

    int day = getfirstday(year);
    
    days[(day + 13) % 7]++;
    day += Jan;
    days[(day + 13) % 7]++;
    day += Feb(year);
    days[(day + 13) % 7]++;
    day += Mar;
    days[(day + 13) % 7]++;
    day += Apr;
    days[(day + 13) % 7]++;
    day += May;
    days[(day + 13) % 7]++;
    day += Jun;
    days[(day + 13) % 7]++;
    day += Jul;
    days[(day + 13) % 7]++;
    day += Aug;
    days[(day + 13) % 7]++;
    day += Sep;
    days[(day + 13) % 7]++;
    day += Oct;
    days[(day + 13) % 7]++;
    day += Nov;
    days[(day + 13) % 7]++;
    day += Jan;
}

int main(){

    int year = 1900, n;
    vector<int> days(7, 0);
    cin >> n;

    for (int i = 0; i < n; i++)
        calculate_year(1900 + i, days);

    cout << days[6] << ' ' << days[0] << ' ' << days[1] << ' ' << days[2] << ' ' << days[3] << ' ' << days[4] << ' ' << days[5] << endl;
    return 0;
}