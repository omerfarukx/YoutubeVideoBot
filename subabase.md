# Ana Tablolar

## users
- Kullanıcı bilgilerini içeren ana tablo
- Gerekli alanlar:
  - id (Primary Key)
  - username
  - email
  - password (hash)
  - created_at
  - updated_at
  - is_active (boolean)
  - role (enum: admin, user)

## stories
- Hikayelerin bilgilerini içeren tablo
- Gerekli alanlar:
  - id (Primary Key)
  - title
  - content
  - user_id (Foreign Key -> users.id)
  - status (enum: draft, processing, completed)
  - created_at
  - updated_at
  - is_deleted (boolean)

## story_images
- Hikayeye ait görsellerin bilgilerini içeren tablo
- Gerekli alanlar:
  - id (Primary Key)
  - story_id (Foreign Key -> stories.id)
  - image_url
  - sequence_number
  - created_at
  - status (enum: pending, completed)

## story_audios
- Hikayeye ait ses dosyalarının bilgilerini içeren tablo
- Gerekli alanlar:
  - id (Primary Key)
  - story_id (Foreign Key -> stories.id)
  - audio_url
  - duration
  - created_at
  - status (enum: pending, completed)

## story_videos
- Hikayeye ait videoların bilgilerini içeren tablo
- Gerekli alanlar:
  - id (Primary Key)
  - story_id (Foreign Key -> stories.id)
  - video_url
  - duration
  - created_at
  - status (enum: processing, completed)

# İlişkiler
- Her kullanıcı (users) birden fazla hikaye (stories) oluşturabilir
- Her hikaye (stories) birden fazla görsel (story_images) içerebilir
- Her hikaye (stories) bir ses dosyası (story_audios) içerebilir
- Her hikaye (stories) bir video (story_videos) içerebilir

# Özellikler
- Tüm ID'ler auto-increment olmalı
- Tarih alanları için timestamp kullanılmalı
- Uygun indexler eklenmeli
- Silinen kayıtlar için soft delete kullanılmalı
