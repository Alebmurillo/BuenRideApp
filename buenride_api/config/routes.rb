Rails.application.routes.draw do
  namespace :api do
        resources :rides, format: :json, except: [:destroy,  :update,:create, :index] do
          collection do
            post 'find_by_user'
            post 'find_by_route'
            post 'find_by_start'
            post 'addRide'
          end
        end
        resources :reviews, format: :json, except: [:destroy,  :update,:create, :index] do
          collection do    
          post 'setReview'
          post 'myReviews'
          post 'getReviews_by_id'
          end
        end
        resources :usuarios, format: :json, except: [:destroy, :update,:create, :index] do
          collection do
            post 'getUsuario_by_id'
            get 'myUsuario'
            post 'login'
            get 'getUsuarios'
            get 'logout'
            post 'registrar'
            
          end
        end
  end
#        resources :usuarios, :defaults => { :format => 'json' }

  # The priority is based upon order of creation: first created -> highest priority.
  # See how all your routes lay out with "rake routes".

  # You can have the root of your site routed with "root"
  # root 'welcome#index'

  # Example of regular route:
  #   get 'products/:id' => 'catalog#view'

  # Example of named route that can be invoked with purchase_url(id: product.id)
  #   get 'products/:id/purchase' => 'catalog#purchase', as: :purchase

  # Example resource route (maps HTTP verbs to controller actions automatically):
  #   resources :products

  # Example resource route with options:
  #   resources :products do
  #     member do
  #       get 'short'
  #       post 'toggle'
  #     end
  #
  #     collection do
  #       get 'sold'
  #     end
  #   end

  # Example resource route with sub-resources:
  #   resources :products do
  #     resources :comments, :sales
  #     resource :seller
  #   end

  # Example resource route with more complex sub-resources:
  #   resources :products do
  #     resources :comments
  #     resources :sales do
  #       get 'recent', on: :collection
  #     end
  #   end

  # Example resource route with concerns:
  #   concern :toggleable do
  #     post 'toggle'
  #   end
  #   resources :posts, concerns: :toggleable
  #   resources :photos, concerns: :toggleable

  # Example resource route within a namespace:
  #   namespace :admin do
  #     # Directs /admin/products/* to Admin::ProductsController
  #     # (app/controllers/admin/products_controller.rb)
  #     resources :products
  #   end
end
