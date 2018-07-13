from django.shortcuts import render

# Create your views here.
from django.http.response import HttpResponse
 
def index_page(request, id):
    return HttpResponse('This is urls test. id = ' + str(id))

def my_page(request):
    return HttpResponse('This is django')
