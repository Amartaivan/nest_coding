#include <iostream>
#include <vector>

using namespace std;

vector<int> merge_sort(vector<int> A){

    int size = A.size();
    vector<int> result = A;

    if (size > 1){
        int midpoint = size / 2;

        vector<int> left_half = merge_sort(vector<int>(A.begin(), A.begin() + midpoint)), 
            right_half = merge_sort(vector<int>(A.begin() + midpoint, A.end()));
        int i = 0, j = 0;

        while (i < left_half.size() && j < right_half.size()){
            if (left_half[i] > right_half[j]){
                result[i + j] = right_half[j];
                j++;
            }
            else {
                result[i + j] = left_half[i];
                i++;
            }
        }
        while (j < right_half.size()){
            result[i + j] = right_half[j];
            j++;
        }
        while (i < left_half.size()){
            result[i + j] = left_half[i];
            i++;
        }
    }
    return result;
}

int main(){

    int n;
    cin >> n;
    vector<int> A(n);
    for (int i = 0; i < n; i++)
        cin >> A[i];
    
    A = merge_sort(A);

    for (int a : A)
        cout << a << ' ';
    cout << endl;
    return 0;
}