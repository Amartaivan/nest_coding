#include <iostream>
#include <vector>
#include <map>

using namespace std;

vector<int> sort(vector<int> a){
    vector<int> result;
    map<int, int> count;

    for (int number : a)
        if (count.find(number) == count.end())
            count.emplace(number, 1);
        else
            count[number]++;

    for (auto number : count)
        for (int i = 0; i < number.second; i++)
            result.push_back(number.first);

    return result;
}

int main(){

    int n;
    cin >> n;

    vector<int> a(n);
    for (int i = 0; i < n; i++)
        cin >> a[i];

    a = sort(a);

    for (int num : a)
        cout << num << ' ';
    cout << endl;

    return 0;
}