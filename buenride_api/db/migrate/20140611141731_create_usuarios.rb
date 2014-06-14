class CreateUsuarios < ActiveRecord::Migration
  def change
    create_table :usuarios do |t|
      t.string :username
      t.string :password
      t.string :token
      t.string :nombre
      t.string :email
      t.string :telefono

      t.timestamps
    end
  end
end
