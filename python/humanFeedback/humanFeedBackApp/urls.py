from django.urls import path
from . import views
 
urlpatterns = [
    path('<int:id>', views.index_page, name='index_page'),
    path('', views.my_page, name='my_page'),
]