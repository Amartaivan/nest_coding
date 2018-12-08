//Solution 2: with hardcoded hidden layer
#include <iostream>
#include <vector>

using namespace std;

class Neuron{
public:
    float value;
    vector<Neuron> inputs;
    vector<float> weights;

    virtual float transfer_function(){
    	float result = 0;
        for (size_t i = 0; i < inputs.size(); i++)
        	result += inputs[i].value * weights[i];
        value = result;
        return value;
    }
    virtual float predict(){
        return transfer_function() >= 0.5 ? 1 : 0;
    }
    
    Neuron(float val){
        value = val;
    }
    Neuron(vector<Neuron> input, vector<float> weight){
        inputs = input;
        weights = weight;
        transfer_function();
    }
    Neuron(){}
};

int main(){
    float val_vodka, val_rain, val_friend;
    cin >> val_vodka >> val_rain >> val_friend;
    Neuron vodka(val_vodka), rain(val_rain), best_friend(val_friend);
    
    vector<Neuron> values = {vodka, rain, best_friend};
    vector<float> weights_hidden1 = {0.25, 0.25, 0};
    vector<float> weights_hidden2 = {0.5, -0.4, 0.9};

    Neuron hidden1(values, weights_hidden1);
    Neuron hidden2(values, weights_hidden2);
    
    vector<Neuron> values_result = {Neuron(hidden1.predict()), Neuron(hidden2.predict())};
    vector<float> weights_result = {-1, 1};
    Neuron result(values_result, weights_result);

    cout << result.predict() << endl;
    return 0;
}
