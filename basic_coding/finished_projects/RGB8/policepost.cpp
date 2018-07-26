#include <iostream>
#include <cmath>

using namespace std;

int main(){
	
	int n, x, i, result, resulti;
	cin >> n;
	int A[n];
	
	for (i = 0; i < n; i++)
		cin >> A[i];
	cin >> x;
	
	for (i = 0; i < n; i++){
		A[i] = abs(A[i] - x);
		if (i == 0 || A[i] <= result){
			result = A[i];
			resulti = i;
		}
	}
	
	cout << resulti + 1 << endl;
	return 0;
}
